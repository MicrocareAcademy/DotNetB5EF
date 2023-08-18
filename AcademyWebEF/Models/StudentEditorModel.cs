using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AcademyWebEF.Models
{
    public class StudentEditorModel
    {
        [Required(ErrorMessage = "Please enter name for the student")]
        [StringLength(50)]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Please enter roll no")]
        [StringLength(20)]
        [Display(Name = "Roll No")]
        public string RollNo { get; set; }

        [Display(Name = "Mobile No")]
        [StringLength(20)]
        public string? Mobile { get; set; }

        [EmailAddress]
        [StringLength(50)]
        [Required(ErrorMessage = "Please enter email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter date of birth")]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [HiddenInput]
        public int StudentID { get; set; }

        [Required(ErrorMessage = "Please select a course")]
        [Display(Name ="Course")]
        public int CourseID { get; set; }

        [IgnoreDataMember]
        public List<SelectListItem> Courses { get; set; }
    }
}
