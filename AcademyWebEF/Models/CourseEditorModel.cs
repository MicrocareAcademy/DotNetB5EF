using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AcademyWebEF.Models
{
    public class CourseEditorModel
    {
        [HiddenInput]
        public int CourseId { get; set; }

        [Required(ErrorMessage ="Please enter course title.")]
        [StringLength(50)]
        [Display(Name ="Course Title")]
        public string CourseTitle { get; set; } = null!;

        [Required(ErrorMessage = "Please enter course duration.")]
        [Display(Name = "Duration(days)")]
        public int DurationInDays { get; set; }

        [Required(ErrorMessage = "Please enter course price.")]
        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
