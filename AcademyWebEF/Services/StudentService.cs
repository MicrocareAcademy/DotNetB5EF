using AcademyWebEF.BusinessEntities;
using AcademyWebEF.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AcademyWebEF.Services
{
    public class StudentService
    {
        public StudentService()
        {
        }

        public IList<Student> FetchStudents()
        {
            var dbContext = new AcademyDbContext();

            return dbContext.Students
                                    .Include(p => p.Course)
                                    .ToList();
        }

        public StudentEditorModel PrepareStudentCreateModel()
        {
            StudentEditorModel model = new StudentEditorModel();

            model.Courses = new List<SelectListItem>();

            var dbContext = new AcademyDbContext();
            var courses = dbContext.Courses.ToList(); // we are getting list of course objects from DB

            // we are looping through courses and will prepare an object of selectListItem and will
            // add to model.Courses

            model.Courses.Add(new SelectListItem { Value = null, Text = "--Select Course--" });

            foreach (var course in courses)
            {
                var courseTitle = $"{course.CourseTitle}/{course.Price} INR";

                var courseItem = new SelectListItem
                {
                    Value = course.CourseId.ToString(),
                    Text = courseTitle
                };

                model.Courses.Add(courseItem);
            }

            return model;
        }

        public Student CreateStudent(StudentEditorModel editorModel, int userId)
        {
            Student student = new Student();
            student.StudentName = editorModel.StudentName;
            student.RollNo = editorModel.RollNo;
            student.Dob = editorModel.DateOfBirth;
            student.MobileNo = editorModel.Mobile;
            student.Email = editorModel.Email;
            student.CourseId = editorModel.CourseID;

            student.UserId = userId;

            var dbContext = new AcademyDbContext();

            // give this object to DBContext  to save the data in the database
            dbContext.Students.Add(student); 

            dbContext.SaveChanges();
            
            return student;
        }

        public StudentEditorModel PrepareStudentUpdateModel(int studentId)
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

            return editorModel;
        }
        public Student UpdateStudent(StudentEditorModel editorModel)
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

            return studentObj;
        }

        public void DeleteOperation(int studentId)
        {
            var dbContext = new AcademyDbContext();

            // get student obj
            var studentObj = dbContext.Students.Where(p => p.StudentId == studentId).FirstOrDefault();

            dbContext.Students.Remove(studentObj);
            dbContext.SaveChanges();
        }
    }
}
