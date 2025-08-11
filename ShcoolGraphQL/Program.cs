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
using FirebaseAdminAuthentication.DependencyInjection.Extensions;
using FirebaseAdmin;
using FirebaseAdminAuthentication.DependencyInjection.Models;

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
                .AddInMemorySubscriptions()
                .AddSorting()
                .AddFiltering()
                .AddProjections()
                .AddAuthorization();


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

            builder.Services.AddSingleton(FirebaseApp.Create());
            builder.Services.AddFirebaseAuthentication();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin", policy =>
                {
                    policy.RequireClaim(FirebaseUserClaimType.EMAIL, "test@gmail.com");
                });
            });
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
