using SchoolGraphQL.Entities.Models;

namespace SchoolGraphQL.Entities.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Department> Departments { get; }
        IGenericRepository<Student> Students { get; }
        IGenericRepository<Course> Courses { get; }

        Task<int> Complete();
    }
}
