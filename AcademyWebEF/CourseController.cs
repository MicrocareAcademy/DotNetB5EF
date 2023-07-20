using AcademyWebEF.BusinessEntities;
using Microsoft.AspNetCore.Mvc;

namespace AcademyWebEF
{
    public class CourseController : Controller
    {
        public IActionResult CoursesList()
        {
            var dbContext = new AcademyDbContext();

            var courses = dbContext.Courses.ToList();

            return View(courses);
        }
    }
}
