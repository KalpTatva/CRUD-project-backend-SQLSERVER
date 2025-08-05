
using System.ComponentModel.DataAnnotations;

public class CourseViewModel
{
    [Required(ErrorMessage = "Course name is required")]
    public string CourseName { get; set; } = null!;

    [Required(ErrorMessage = "Course content is required")]
    public string CourseContent {get;set;} = null!;

    [Required(ErrorMessage = "Credits are required")]
    [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid Credits input.")]
    public int Credits {get;set;}

    [Required(ErrorMessage = "Department is required")]
    public string Department {get;set;} = null!;

    public int UserId {get;set;}
    public int CourseId {get;set;}
}