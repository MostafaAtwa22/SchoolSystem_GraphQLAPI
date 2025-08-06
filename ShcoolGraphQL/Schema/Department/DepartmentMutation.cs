using HotChocolate.Subscriptions;
using SchoolGraphQL.Entities.Dtos;
using SchoolGraphQL.Entities.Interfaces;
using ShcoolGraphQL.Schema;

namespace SchoolGraphQL.Schema.Department
{
    [ExtendObjectType("Mutation")]
    public class DepartmentMutation
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentMutation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DepartmentDto> CreateDepartment(DepartmentDto dto, [Service] ITopicEventSender topicEventSender)
        {
            var department = new SchoolGraphQL.Entities.Models.Department
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _unitOfWork.Departments.AddAsync(department);
            await _unitOfWork.Complete();

            await topicEventSender.SendAsync(nameof(Subscription.DepartmentCreate), dto);
            return dto;
        }

        public async Task<DepartmentDto> UpdateDepartment(DepartmentDto dto, int id, [Service] ITopicEventSender topicEventSender)
        {
            try
            {
                var department = await _unitOfWork.Departments.GetByIdAsync(id);
                if (department is null)
                    throw new GraphQLException(new Error("Department not found", "DEPARTMENT_NOT_FOUND"));

                department.Name = dto.Name;
                department.Description = dto.Description;

                _unitOfWork.Departments.Update(department);
                await _unitOfWork.Complete();

                var departmentUpdateTopic = $"{department.Id}_{nameof(Subscription.DepartmentUpdate)}";
                await topicEventSender.SendAsync(departmentUpdateTopic, dto);
                return dto;
            }
            catch (Exception ex)
            {
                // Return detailed GraphQL error
                throw new GraphQLException(
                    ErrorBuilder.New()
                        .SetMessage("Failed to update department: " + ex.Message)
                        .SetCode("UPDATE_DEPARTMENT_FAILED")
                        .Build());
            }
        }


        public async Task<DepartmentDto> DeleteDepartment(int id, [Service] ITopicEventSender topicEventSender)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department is null)
                throw new GraphQLException(new Error("Department not found", "DEPARTMENT_NOT_FOUND"));

            _unitOfWork.Departments.Delete(department);
            await _unitOfWork.Complete();

            var dto = new DepartmentDto
            {
                Name = department.Name,
                Description = department.Description
            };

            var departmentDeleteTopic = $"{department.Id}_{nameof(Subscription.DepartmentDelete)}";
            await topicEventSender.SendAsync(departmentDeleteTopic, dto);
            return dto;
        }
    }
}
