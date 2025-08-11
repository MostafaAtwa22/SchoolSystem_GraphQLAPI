using FirebaseAdminAuthentication.DependencyInjection.Models;
using HotChocolate.Authorization;
using HotChocolate.Subscriptions;
using SchoolGraphQL.Entities.Dtos;
using SchoolGraphQL.Entities.Interfaces;
using ShcoolGraphQL.Schema;
using System.Security.Claims;

namespace SchoolGraphQL.Schema.Course
{
    [Authorize(Policy = "IsAdmin")]
    [ExtendObjectType("Mutation")]
    public class CourseMutation
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseMutation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CourseDto> CreateCourse(CourseDto dto, [Service]ITopicEventSender topicEventSender, ClaimsPrincipal claimsPrincipal)
        {
            string userId = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.ID)!;
            string email = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.EMAIL)!;
            string username = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.USERNAME)!;
            string verified = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.EMAIL_VERIFIED)!;
            var course = new SchoolGraphQL.Entities.Models.Course
            {
                Title = dto.Title,
                DepartmentId = dto.DepartmentId
            };

            await _unitOfWork.Courses.AddAsync(course);
            await _unitOfWork.Complete();

            await topicEventSender.SendAsync(nameof(Subscription.CourseCreate), dto);
            return dto;
        }

        public async Task<CourseDto> UpdateCourse(CourseDto dto, int id, [Service] ITopicEventSender topicEventSender)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            if (course is null)
                throw new GraphQLException(new Error("Course not found", "COURSE_NOT_FOUND"));

            course.Title = dto.Title;
            course.DepartmentId = dto.DepartmentId;

            _unitOfWork.Courses.Update(course);
            await _unitOfWork.Complete();

            var courseUpdateTopic = $"{id}_{nameof(Subscription.CourseUpdate)}";
            await topicEventSender.SendAsync(courseUpdateTopic, dto);
            return dto;
        }

        public async Task<CourseDto> DeleteCourse(int id, [Service] ITopicEventSender topicEventSender)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            if (course is null)
                throw new GraphQLException(new Error("Course not found", "COURSE_NOT_FOUND"));

            _unitOfWork.Courses.Delete(course);
            await _unitOfWork.Complete();

            var dto = new CourseDto
            {
                Title = course.Title,
                DepartmentId = course.DepartmentId
            };

            var courseDeleteTopic = $"{id}_{nameof(Subscription.CourseDelete)}";
            await topicEventSender.SendAsync(courseDeleteTopic, dto);
            return dto;
        }
    }
}
