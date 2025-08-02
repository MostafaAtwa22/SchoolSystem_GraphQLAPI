using System.ComponentModel.DataAnnotations;

namespace SchoolGraphQL.Entities.Dtos
{
    public class StudentDto
    {
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        public int DepartmentId { get; set; }
    }
}
