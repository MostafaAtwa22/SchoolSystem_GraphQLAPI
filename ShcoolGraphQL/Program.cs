using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolGraphQL.DataAccess.Data;
using SchoolGraphQL.DataAccess.Repositories;
using SchoolGraphQL.Entities.Interfaces;
using SchoolGraphQL.Entities.Models;
using ShcoolGraphQL.Schema.Department;
using ShcoolGraphQL.Schema.Student;
using ShcoolGraphQL.Schema.Course;
using ShcoolGraphQL.Schema;
using SchoolGraphQL.Schema.Course;
using SchoolGraphQL.Schema.Department;

namespace ShcoolGraphQL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddTypeExtension<DepartmentQuery>()
                .AddTypeExtension<CourseQuery>()
                .AddTypeExtension<StudentQuery>()
                .AddTypeExtension<StudentMutation>()
                .AddTypeExtension<CourseMutation>()
                .AddTypeExtension<DepartmentMutation>()
                .AddInMemorySubscriptions();

            var constr = builder.Configuration.GetConnectionString("Default")
                    ?? throw new InvalidOperationException("No Connection String");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(constr);
            });

            // Add Identity services
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add Authentication services
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            var app = builder.Build();

            // ✅ Enable dev exception page (optional but helpful)
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Seed data
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
                SeedData.Seed(context);
            }

            // WebSockets for subscriptions
            app.UseWebSockets();
            app.UseRouting();

            // CORS middleware
            app.UseCors("AllowAll");

            // Authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGraphQL();

            app.Run();
        }
    }
}
