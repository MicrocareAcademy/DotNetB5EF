using AcademyWebEF.BusinessEntities;
using AcademyWebEF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AcademyWebEF
{
    [Authorize(Roles = Roles.Admin + "," + Roles.Student)]
    public class StudentController : Controller
    {
        public IActionResult StudentsList()
        {
            var dbContext = new AcademyDbContext();

            var students = dbContext.Students
                                    .Include(p=>p.Course)
                                    .ToList();

            return View(students);
        }

        [HttpGet]
        public IActionResult StudentEditor()
        {
            var model = new StudentEditorModel();
            model.Courses = new List<SelectListItem>();

            var dbContext = new AcademyDbContext();
            var courses = dbContext.Courses.ToList(); // we are getting list of course objects from DB

            // we are looping through courses and will prepare an object of selectListItem and will
            // add to model.Courses

            model.Courses.Add(new SelectListItem { Value = null, Text = "--Select Course--" });

            foreach (var course in courses)  
            {
                var courseTitle = $"{course.CourseTitle}/{course.Price} INR";

                var courseItem = new SelectListItem { Value = course.CourseId.ToString(), 
                                                       Text = courseTitle
                                                    };

                model.Courses.Add(courseItem);
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(StudentEditorModel editorModel)
        {
            ModelState.Remove("Courses");

            if (ModelState.IsValid)
            {
                //model binding - automatically

                var dbContext = new AcademyDbContext();

                //Create Student User Account
                User user = new User
                {
                    Email = editorModel.Email,
                    UserName = editorModel.RollNo,
                    Password = "123456",
                    Role = Roles.Student
                };
                dbContext.Users.Add(user); // give this object to DBContext  to save the data in the database

                dbContext.SaveChanges(); 



                // create an object of Student Entity Class 
                Student student = new Student();
                student.StudentName = editorModel.StudentName;
                student.RollNo = editorModel.RollNo;
                student.Dob = editorModel.DateOfBirth;
                student.MobileNo = editorModel.Mobile;
                student.Email = editorModel.Email;
                student.CourseId = editorModel.CourseID;
                student.UserId = user.UserId;

                dbContext.Students.Add(student); // give this object to DBContext  to save the data in the database

                dbContext.SaveChanges();



                return RedirectToAction("StudentsList");
            }
            else
            {
                ModelState.AddModelError("", "Student record not created, please fix errors and save again!");

                return View("StudentEditor", editorModel);
            }

        }

        [HttpGet]
        public IActionResult EditStudent(int studentId) // binding primitive type
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
        public IActionResult Update(StudentEditorModel editorModel) // binding complex type
        {
            if (ModelState.IsValid)
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
            else
            {
                ModelState.AddModelError("", "Student record not updated, please fix errors and save again!");

                return View("EditStudent", editorModel);
            }
        }

        [HttpGet]
        public ViewResult StudentRO(int studentId)
        {
            var dbContext = new AcademyDbContext();

            // get student obj
            var studentObj = dbContext.Students
                                      .Include(p=>p.Course)
                                      .Include(p=>p.User)
                                      .FirstOrDefault(p => p.StudentId == studentId);

            return View(studentObj);
        }

        [HttpPost]
        public JsonResult DeleteStudent(int studId)
        {
            try
            {
                var dbContext = new AcademyDbContext();

                // get student obj
                var studentObj = dbContext.Students.Where(p => p.StudentId == studId).FirstOrDefault();

                dbContext.Students.Remove(studentObj);
                dbContext.SaveChanges();

                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }

    }
}
