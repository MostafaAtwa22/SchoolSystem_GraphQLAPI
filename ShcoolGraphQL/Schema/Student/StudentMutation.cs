using HotChocolate.Authorization;
using HotChocolate.Subscriptions;
using SchoolGraphQL.Entities.Dtos;
using SchoolGraphQL.Entities.Interfaces;
using SchoolGraphQL.Entities.Models;

namespace ShcoolGraphQL.Schema.Student
{
    [Authorize]
    [ExtendObjectType("Mutation")]
    public class StudentMutation
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentMutation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StudentDto> CreateStudent (StudentDto dto, [Service]ITopicEventSender topicEventSender)
        {
            var student = new SchoolGraphQL.Entities.Models.Student
            {
                Name = dto.Name,
                Age = dto.Age,
                DepartmentId = dto.DepartmentId,
                Email = dto.Email,
            };
            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.Complete();

            await topicEventSender.SendAsync(nameof(Subscription.StudentCreate), dto);
            return dto;
        }

        public async Task<StudentDto> UpdateStudent(StudentDto dto, int id, [Service]ITopicEventSender topicEventSender)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student is null)
                throw new GraphQLException(new Error("Student not found", "STUDENT_NOT_FOUND"));

            student.Name = dto.Name;
            student.Age = dto.Age;
            student.DepartmentId = dto.DepartmentId;
            student.Email = dto.Email;

            _unitOfWork.Students.Update(student);
            await _unitOfWork.Complete();

            var updateStudentTopic = $"{student.Id}_{nameof(Subscription.StudentUpdate)}";
            await topicEventSender.SendAsync(updateStudentTopic, dto);
            return dto;
        }

        public async Task<StudentDto> DeleteStudent(int id, [Service]ITopicEventSender topicEventSender)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student is null)
                throw new GraphQLException(new Error("Student not found", "STUDENT_NOT_FOUND"));

            _unitOfWork.Students.Delete(student);
            await _unitOfWork.Complete();

            var dto = new StudentDto
            {
                Name = student.Name,
                Age = student.Age,
                DepartmentId = student.DepartmentId,
                Email = student.Email
            };
            var deleteStudentTopic = $"{student.Id}_{nameof(Subscription.StudentDelete)}";
            await topicEventSender.SendAsync(deleteStudentTopic, dto);
            return dto;
        }
    }
}
