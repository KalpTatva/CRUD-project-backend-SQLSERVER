using System.Linq;
using crud.repository.Models;
using webapi.Repository.Interfaces;

namespace webapi.Repository.Implementations;

public class CourseRepository : ICourseRepository
{
    private readonly StudentCourseContext _context;

    public CourseRepository(StudentCourseContext context)
    {
        _context = context;
    }


    public List<Course> GetAllCourses()
    {
        try
        {
            return _context.Courses.Where(c => c.IsDeleted == false).OrderByDescending(c => c.Courseid).ToList();
        }
        catch (Exception e)
        {
            throw new Exception($"Error occured while fatching courses : {e.Message} ");
        }
    }

    public Course? GetCourseById(int id)
    {
        try
        {
            return _context.Courses.FirstOrDefault(c => c.Courseid == id);
        }
        catch (Exception e)
        {
            throw new Exception($"Error occured while fatching course : {e.Message} ");
        }
    }

    public void AddCourse(Course course)
    {
        try
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception($"Error occured while adding course : {e.Message}");
        }
    }
    public void EditCourse(Course course)
    {
        try
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception($"Error occured while adding course : {e.Message}");
        }
    }

}
