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

        [HttpGet]
        public IActionResult EditStudent(int studentId)
        {
            var dbContext = new AcademyDbContext();

            // get student obj
            var studentObj = dbContext.Students.Where(p => p.StudentId == studentId).FirstOrDefault();

            // create an object of model class
            // and bind the data from student obj
            var editorModel = new StudentEditorModel();
            editorModel.StudentName = studentObj.StudentName;
            editorModel.RollNo = studentObj.RollNo;
            editorModel.DateOfBirth = studentObj.Dob;
            editorModel.Email = studentObj.Email;
            editorModel.Mobile = studentObj.MobileNo;
            editorModel.StudentID = studentObj.StudentId;

            return View(editorModel);
        }

        [HttpPost]
        public IActionResult Update(StudentEditorModel editorModel)
        {
            var dbContext = new AcademyDbContext();

            //fetching the student obj from database
            var studentObj = dbContext.Students.Where(p => p.StudentId == editorModel.StudentID).FirstOrDefault();

            // updating the details of existing student
            studentObj.StudentName = editorModel.StudentName;
            studentObj.RollNo = editorModel.RollNo;
            studentObj.Dob = editorModel.DateOfBirth;
            studentObj.MobileNo = editorModel.Mobile;
            studentObj.Email = editorModel.Email;

            dbContext.Students.Update(studentObj); // update student obj

            dbContext.SaveChanges(); // generate update statement

            return RedirectToAction("StudentsList");
        }

        [HttpGet]
        public ViewResult StudentRO(int studentId)
        {
            var dbContext = new AcademyDbContext();

            // get student obj
            var studentObj = dbContext.Students.Where(p => p.StudentId == studentId).FirstOrDefault();

            return View(studentObj);
        }

    }
}
