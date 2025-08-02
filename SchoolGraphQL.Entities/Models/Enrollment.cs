namespace SchoolGraphQL.Entities.Models
{
    public class Enrollment
    {
        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public virtual Student Student { get; set; } = default!;

        public virtual Course Course { get; set; } = default!;
    }
}
