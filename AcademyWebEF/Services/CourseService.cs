using AcademyWebEF.BusinessEntities;
using AcademyWebEF.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademyWebEF.Services
{
    public class CourseService
    {
        private readonly AcademyDbContext _dbContext;

        public CourseService()
        {
            _dbContext = new AcademyDbContext();
        }

        public IList<Course> GetAllCourses()
        {
           return _dbContext.Courses.ToList();
        }

        public Course? GetCourseById(int courseId)
        {
           return _dbContext.Courses.FirstOrDefault(p=>p.CourseId == courseId);
        }

        public Course CreateCourse(CourseEditorModel model)
        {
            Course course = new Course
            {
                CourseTitle = model.CourseTitle,
                DurationInDays = model.DurationInDays,
                Price = model.Price,
                IsActive = model.IsActive
            };

            _dbContext.Courses.Add(course);

            _dbContext.SaveChanges();

            return course;
        }
    }
}
