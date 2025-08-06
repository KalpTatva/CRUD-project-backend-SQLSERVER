using System;
using System.Collections.Generic;

namespace crud.repository.Models;

public partial class Course
{
    public int Courseid { get; set; }

    public string CourseName { get; set; } = null!;

    public string CourseContent { get; set; } = null!;

    public int Credits { get; set; }

    public string Department { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public int? CreatedById { get; set; }

    public DateTime? EditedAt { get; set; }

    public int? EditedById { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? DeletedById { get; set; }

    public bool? IsDeleted { get; set; }

    internal List<Course> ToListAsync()
    {
        throw new NotImplementedException();
    }
}
