using System;
using System.Collections.Generic;

namespace AcademyWebEF.BusinessEntities;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseTitle { get; set; } = null!;

    public int DurationInDays { get; set; }

    public decimal Price { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
