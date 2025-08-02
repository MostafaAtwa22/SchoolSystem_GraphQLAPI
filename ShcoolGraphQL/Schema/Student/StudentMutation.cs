using SchoolGraphQL.Entities.Dtos;
using SchoolGraphQL.Entities.Interfaces;
using SchoolGraphQL.Entities.Models;

namespace ShcoolGraphQL.Schema.Student
{
    [ExtendObjectType("Mutation")]
    public class StudentMutation
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentMutation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StudentDto> CreateStudent (StudentDto dto)
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

            return dto;
        }

        public async Task<StudentDto> UpdateStudent(StudentDto dto, int id)
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

            return dto;
        }

        public async Task<StudentDto> DeleteStudent(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student is null)
                throw new GraphQLException(new Error("Student not found", "STUDENT_NOT_FOUND"));

            _unitOfWork.Students.Delete(student);
            await _unitOfWork.Complete();

            return new StudentDto
            {
                Name = student.Name,
                Age = student.Age,
                DepartmentId = student.DepartmentId,
                Email = student.Email
            };
        }
    }
}
