using SchoolGraphQL.Entities.Dtos;
using SchoolGraphQL.Entities.Interfaces;

namespace ShcoolGraphQL.Schema.Department
{
    [ExtendObjectType(nameof(Query))]
    public class DepartmentQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartments()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync();

            var departmentsDto = new List<DepartmentDto>();

            foreach (var dept in departments)
            {
                var departmentDto = new DepartmentDto
                {
                    Name = dept.Name,
                    Description = dept.Description,
                };
                departmentsDto.Add(departmentDto);
            }

            return departmentsDto;
        }

        public async Task<DepartmentDto> GetDepartmentById(int id)
        {
            var dept = await _unitOfWork.Departments.FindAsync(i => i.Id == id);

            if (dept is null)
                throw new GraphQLException(new Error("This Department is not Exists"));

            var departmentDto = new DepartmentDto
            {
                Name = dept.Name,
                Description = dept.Description,
            };

            return departmentDto;
        }
    }
}
