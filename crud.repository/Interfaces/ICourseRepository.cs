using crud.repository.Models;

namespace webapi.Repository.Interfaces;

public interface ICourseRepository
{
    List<Course> GetAllCourses();
    Course? GetCourseById(int id);
    void AddCourse(Course course);
    void EditCourse(Course course);
}
