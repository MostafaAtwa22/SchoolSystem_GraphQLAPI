using SchoolGraphQL.Entities.Dtos;
using SchoolGraphQL.Entities.Interfaces;

namespace SchoolGraphQL.Schema.Course
{
    [ExtendObjectType("Mutation")]
    public class CourseMutation
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseMutation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CourseDto> CreateCourse(CourseDto dto)
        {
            var course = new SchoolGraphQL.Entities.Models.Course
            {
                Title = dto.Title,
                DepartmentId = dto.DepartmentId
            };

            await _unitOfWork.Courses.AddAsync(course);
            await _unitOfWork.Complete();

            return dto;
        }

        public async Task<CourseDto> UpdateCourse(CourseDto dto, int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            if (course is null)
                throw new GraphQLException(new Error("Course not found", "COURSE_NOT_FOUND"));

            course.Title = dto.Title;
            course.DepartmentId = dto.DepartmentId;

            _unitOfWork.Courses.Update(course);
            await _unitOfWork.Complete();

            return dto;
        }

        public async Task<CourseDto> DeleteCourse(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            if (course is null)
                throw new GraphQLException(new Error("Course not found", "COURSE_NOT_FOUND"));

            _unitOfWork.Courses.Delete(course);
            await _unitOfWork.Complete();

            return new CourseDto
            {
                Title = course.Title,
                DepartmentId = course.DepartmentId
            };
        }
    }
}
