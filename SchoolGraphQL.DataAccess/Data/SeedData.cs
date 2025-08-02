using SchoolGraphQL.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace SchoolGraphQL.DataAccess.Data
{
    public static class SeedData
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Check if data already exists
            if (context.Departments.Any())
            {
                return; // Data already seeded
            }

            // Seed Departments
            var departments = new List<Department>
            {
                new Department { Name = "Computer Science", Description = "Study of computers and computational systems" },
                new Department { Name = "Mathematics", Description = "Study of numbers, quantities, shapes, and patterns" },
                new Department { Name = "Physics", Description = "Study of matter, energy, and their interactions" },
                new Department { Name = "Chemistry", Description = "Study of substances and their properties" },
                new Department { Name = "Biology", Description = "Study of living organisms and life processes" },
                new Department { Name = "Engineering", Description = "Application of scientific principles to design and build" },
                new Department { Name = "Business Administration", Description = "Study of business management and operations" },
                new Department { Name = "Psychology", Description = "Study of mind and behavior" },
                new Department { Name = "History", Description = "Study of past events and human societies" },
                new Department { Name = "Literature", Description = "Study of written works and creative expression" }
            };

            context.Departments.AddRange(departments);
            context.SaveChanges();

            // Seed Students
            var students = new List<Student>();
            var firstNames = new[] { "John", "Jane", "Michael", "Sarah", "David", "Emily", "James", "Jessica", "Robert", "Amanda", 
                                   "William", "Ashley", "Christopher", "Samantha", "Daniel", "Megan", "Matthew", "Nicole", "Joshua", "Stephanie",
                                   "Andrew", "Lauren", "Ryan", "Rachel", "Brandon", "Kayla", "Justin", "Amber", "Tyler", "Brittany",
                                   "Kevin", "Danielle", "Steven", "Melissa", "Brian", "Heather", "Timothy", "Tiffany", "Jeffrey", "Crystal",
                                   "Mark", "Michelle", "Paul", "Kimberly", "Donald", "Lisa", "Kenneth", "Jennifer", "Anthony", "Elizabeth" };
            
            var lastNames = new[] { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
                                  "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin",
                                  "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson",
                                  "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores",
                                  "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts" };

            var random = new Random();
            
            for (int i = 0; i < 50; i++)
            {
                var firstName = firstNames[i % firstNames.Length];
                var lastName = lastNames[i % lastNames.Length];
                var departmentId = random.Next(1, 11); // 1-10 for departments
                
                students.Add(new Student
                {
                    Name = $"{firstName} {lastName}",
                    Email = $"{firstName.ToLower()}.{lastName.ToLower()}@school.edu",
                    Age = random.Next(18, 25),
                    DepartmentId = departmentId
                });
            }

            context.Students.AddRange(students);
            context.SaveChanges();

            // Seed Courses
            var courses = new List<Course>
            {
                // Computer Science Courses
                new Course { Title = "Introduction to Programming", DepartmentId = 1 },
                new Course { Title = "Data Structures and Algorithms", DepartmentId = 1 },
                new Course { Title = "Database Systems", DepartmentId = 1 },
                new Course { Title = "Web Development", DepartmentId = 1 },
                new Course { Title = "Software Engineering", DepartmentId = 1 },
                
                // Mathematics Courses
                new Course { Title = "Calculus I", DepartmentId = 2 },
                new Course { Title = "Linear Algebra", DepartmentId = 2 },
                new Course { Title = "Statistics", DepartmentId = 2 },
                new Course { Title = "Discrete Mathematics", DepartmentId = 2 },
                new Course { Title = "Differential Equations", DepartmentId = 2 },
                
                // Physics Courses
                new Course { Title = "Classical Mechanics", DepartmentId = 3 },
                new Course { Title = "Electromagnetism", DepartmentId = 3 },
                new Course { Title = "Quantum Physics", DepartmentId = 3 },
                new Course { Title = "Thermodynamics", DepartmentId = 3 },
                new Course { Title = "Optics", DepartmentId = 3 },
                
                // Chemistry Courses
                new Course { Title = "General Chemistry", DepartmentId = 4 },
                new Course { Title = "Organic Chemistry", DepartmentId = 4 },
                new Course { Title = "Physical Chemistry", DepartmentId = 4 },
                new Course { Title = "Analytical Chemistry", DepartmentId = 4 },
                new Course { Title = "Biochemistry", DepartmentId = 4 }
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();

            Console.WriteLine("Database seeded successfully!");
            Console.WriteLine($"- {departments.Count} Departments created");
            Console.WriteLine($"- {students.Count} Students created");
            Console.WriteLine($"- {courses.Count} Courses created");
        }
    }
} 