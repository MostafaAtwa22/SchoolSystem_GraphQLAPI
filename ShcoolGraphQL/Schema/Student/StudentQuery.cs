using SchoolGraphQL.Entities.Dtos;
using SchoolGraphQL.Entities.Interfaces;

namespace ShcoolGraphQL.Schema.Student
{
    [ExtendObjectType(nameof(Query))]
    public class StudentQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<StudentDto>> GetAllStudents()
        {
            var students = await _unitOfWork.Students.GetAllAsync();

            var studentsDto = new List<StudentDto>();

            foreach (var st in students)
            {
                var studentDto = new StudentDto
                {
                    Name = st.Name,
                    Age = st.Age,
                    DepartmentId = st.DepartmentId,
                    Email = st.Email
                };
                studentsDto.Add(studentDto);
            }

            return studentsDto;
        }

        public async Task<StudentDto> GetStudentById(int id)
        {
            var st = await _unitOfWork.Students.FindAsync(i => i.Id == id);

            if (st is null)
                throw new GraphQLException(new Error("This Student is not Exists"));
            var studentDto = new StudentDto
            {
                Name = st.Name,
                Age = st.Age,
                DepartmentId = st.DepartmentId,
                Email = st.Email
            };

            return studentDto;
        }
    }
}
