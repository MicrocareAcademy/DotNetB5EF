using AcademyWebEF.BusinessEntities;
using AcademyWebEF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademyWebEF
{
    [Authorize(Roles = Roles.Admin)]
    public class CourseController : Controller
    {
        public IActionResult CoursesList()
        {
            var dbContext = new AcademyDbContext();

            var courses = dbContext.Courses.ToList();

            return View(courses);
        }

        public IActionResult Create()
        {
            return View(new CourseEditorModel());
        }

        [HttpPost]
        public ActionResult SaveCourse(CourseEditorModel model)
        {
            if (ModelState.IsValid)
            {
                Course course = new Course
                {
                    CourseTitle = model.CourseTitle,
                    DurationInDays = model.DurationInDays,
                    Price = model.Price,
                    IsActive = model.IsActive
                };

                var dbContext = new AcademyDbContext();
                dbContext.Courses.Add(course);
                dbContext.SaveChanges();

                return RedirectToAction("CoursesList");

            }
            else
            {
                ModelState.AddModelError("", "Fail to create course, please validate the data.");
                return View("Create", model);
            }
        }
    }
}
