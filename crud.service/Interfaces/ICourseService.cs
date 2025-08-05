using crud.repository.Models;
using webapi.Repository.ViewModels;

namespace webapi.Service.Interfaces;

public interface ICourseService
{
    List<Course> GetAllCourses();
    Course? GetCourseById(int id);
    ResponseViewModel DeleteCourseById(int id);
    ResponseViewModel AddCourse(CourseViewModel courseViewModel);
    ResponseViewModel EditCourse(CourseViewModel courseViewModel);
}
