using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolGraphQL.Entities.Models;

namespace SchoolGraphQL.DataAccess.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId);
            builder.HasMany(c => c.Students)
                   .WithMany(s => s.Courses)
                   .UsingEntity<Enrollment>(
                        j => j
                            .HasOne(e => e.Student)
                            .WithMany(s => s.Enrollments)
                            .HasForeignKey(e => e.StudentId)
                            .OnDelete(DeleteBehavior.Restrict),
                        j => j
                            .HasOne(e => e.Course)
                            .WithMany(c => c.Enrollments)
                            .HasForeignKey(e => e.CourseId)
                            .OnDelete(DeleteBehavior.Restrict),
                        j =>
                        {
                            j.HasKey(e => new { e.StudentId, e.CourseId });
                        }
                    );
        }
    }
    
}
