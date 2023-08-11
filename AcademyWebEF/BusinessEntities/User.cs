using System;
using System.Collections.Generic;

namespace AcademyWebEF.BusinessEntities;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Role { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
