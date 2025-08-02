using SchoolGraphQL.DataAccess.Data;
using SchoolGraphQL.Entities.Interfaces;
using SchoolGraphQL.Entities.Models;

namespace SchoolGraphQL.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGenericRepository<Department> Departments { get; }
        public IGenericRepository<Student> Students { get; }
        public IGenericRepository<Course> Courses { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Departments = new GenericRepository<Department>(_context);
            Students = new GenericRepository<Student>(_context);
            Courses = new GenericRepository<Course>(_context);
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
