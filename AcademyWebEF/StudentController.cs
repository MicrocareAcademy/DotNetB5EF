using AcademyWebEF.BusinessEntities;
using AcademyWebEF.Models;
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

        [HttpGet]
        public IActionResult StudentEditor()
        {
            var model = new StudentEditorModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(StudentEditorModel editorModel)
        {
            //model binding - automatically

            // create an object of Student Entity Class 
            Student student = new Student();
            student.StudentName = editorModel.StudentName;
            student.RollNo = editorModel.RollNo;
            student.Dob = editorModel.DateOfBirth;
            student.MobileNo = editorModel.Mobile;
            student.Email = editorModel.Email;

            // give this object to DBContext  to save the data in the database
            var dbContext = new AcademyDbContext();
            dbContext.Students.Add(student);

            dbContext.SaveChanges(); // generate insert statement

            return RedirectToAction("StudentsList");
        }
    }
}
