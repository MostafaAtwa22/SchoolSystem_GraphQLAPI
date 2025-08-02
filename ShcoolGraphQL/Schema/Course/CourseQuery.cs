using SchoolGraphQL.Entities.Dtos;
using SchoolGraphQL.Entities.Interfaces;

namespace ShcoolGraphQL.Schema.Course
{
    [ExtendObjectType(nameof(Query))]
    public class CourseQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCourses()
        {
            var courses = await _unitOfWork.Courses.GetAllAsync();
            
            var coursesDto = new List<CourseDto>();

            foreach (var course in courses)
            {
                var coursedto = new CourseDto
                {
                    DepartmentId = course.DepartmentId,
                    Title = course.Title,
                };
                coursesDto.Add(coursedto);
            }

            return coursesDto;
        }

        public async Task<CourseDto> GetCourseById(int id)
        {
            var course = await _unitOfWork.Courses.FindAsync(i => i.Id == id);

            if (course is null)
                throw new GraphQLException(new Error("This course is not Exists"));

            var dto = new CourseDto
            {
                DepartmentId = course.DepartmentId,
                Title = course.Title,
            };

            return dto;
        }
    }
}
