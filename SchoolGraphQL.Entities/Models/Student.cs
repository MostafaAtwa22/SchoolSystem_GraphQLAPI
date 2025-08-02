using System.ComponentModel.DataAnnotations;

namespace SchoolGraphQL.Entities.Models
{
    public class Student : BaseClass
    {
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; } = default!;

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
