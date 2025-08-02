using System.ComponentModel.DataAnnotations;
using System.Data;

namespace SchoolGraphQL.Entities.Models
{
    public class Department : BaseClass
    {
        [Required]
        [MinLength(2), MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
