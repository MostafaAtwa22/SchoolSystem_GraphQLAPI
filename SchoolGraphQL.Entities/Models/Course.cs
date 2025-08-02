using System.ComponentModel.DataAnnotations;

namespace SchoolGraphQL.Entities.Models
{
    public class Course : BaseClass
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public Department Department { get; set; } = default!;

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
