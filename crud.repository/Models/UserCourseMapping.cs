using System;
using System.Collections.Generic;

namespace crud.repository.Models;

public partial class UserCourseMapping
{
    public int MappingId { get; set; }

    public int Courseid { get; set; }

    public int UserId { get; set; }

    public int? CourseStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedById { get; set; }

    public DateTime? EditedAt { get; set; }

    public int? EditedById { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? DeletedById { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
