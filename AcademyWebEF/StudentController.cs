using AcademyWebEF.BusinessEntities;
using Microsoft.AspNetCore.Mvc;

namespace AcademyWebEF
{
    public class StudentController : Controller
    {
        public IActionResult StudentsList()
        {
            var dbContext = new AcademyDbContext();

            var students = dbContext.Students.ToList();

            return View(students);
        }
    }
}
