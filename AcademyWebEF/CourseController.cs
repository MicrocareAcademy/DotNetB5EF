using AcademyWebEF.BusinessEntities;
using AcademyWebEF.Models;
using AcademyWebEF.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademyWebEF
{
    [Authorize(Roles = Roles.Admin)]
    public class CourseController : Controller
    {
        private readonly CourseService courseService;

        public CourseController()
        {
            courseService = new CourseService();
        }

        public IActionResult CoursesList()
        {
            return View(courseService.GetAllCourses());
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

                courseService.CreateCourse(model);

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
