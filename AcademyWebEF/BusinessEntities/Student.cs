using System;
using System.Collections.Generic;

namespace AcademyWebEF.BusinessEntities;

public partial class Student
{
    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string RollNo { get; set; } = null!;

    public string? MobileNo { get; set; }

    public string? Email { get; set; }

    public int? CourseId { get; set; }

    public int? UserId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual User? User { get; set; }
}
