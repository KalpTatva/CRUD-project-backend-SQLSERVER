using System.Data;
using System.Linq;
using crud.repository.Models;
using Dapper;
using webapi.Repository.Interfaces;

namespace webapi.Repository.Implementations;

public class CourseRepository : ICourseRepository
{
    private readonly StudentCourseContext _context;
    private IDbConnection _dbConnection { get; }

    public CourseRepository(StudentCourseContext context, IDbConnection dbConnection)
    {
        _context = context;
        _dbConnection = dbConnection;

    }


    public async Task<List<Course>> GetAllCourses()
    {
        try
        {
            // for multiple row result, use QueryAsync<T> method
            string query = "exec AllCourses";
            var result = await _dbConnection.QueryAsync<Course>(query);
            return result.ToList();

            // return _context.Courses.Where(c => c.IsDeleted == false).OrderByDescending(c => c.Courseid).ToList();
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
            // for single row result, use QuerySingle<T> or QuerySingleAsync<T> method
            string query = "select * from udfGetCourseById(@CourseId);";
            var parameters = new { CourseId = id };
            var result = _dbConnection.QuerySingle<Course>(query, parameters);
            return result;

            // return _context.Courses.FirstOrDefault(c => c.Courseid == id);
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
            string query = "exec AddCourse @CourseName, @CourseContent, @Credits, @Department, @CreatedById";
            var parameters = new
            {
                CourseName = course.CourseName,
                CourseContent = course.CourseContent,
                Credits = course.Credits,
                Department = course.Department,
                CreatedById = course.CreatedById
            };
            var result = _dbConnection.Query(query, parameters);

            // _context.Courses.Add(course);
            // _context.SaveChanges();
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
            string query = "UpdateCourse @CourseId, @UserId, @CourseName, @CourseContent, @Credits, @Department";
            var parameters = new
            {
                CourseId = course.Courseid,
                UserId = course.EditedById,
                CourseName = course.CourseName,
                CourseContent = course.CourseContent,
                Credits = course.Credits,
                Department = course.Department,
            };
            var result = _dbConnection.Query(query, parameters);
            // _context.Courses.Update(course);
            // _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception($"Error occured while adding course : {e.Message}");
        }
    }

    public void DeleteCourse(int courseid, int userid)
    {
        try
        {
            string query = "exec DeleteCourseById @CourseId, @DeleteById;";
            var parameters = new
            {
                CourseId = courseid,
                DeleteById = userid,
            };
            var result = _dbConnection.Query(query, parameters);
        }
        catch (Exception e)
        {
            throw new Exception($"Error occured while deleting course : {e.Message}");
        }
    }

}
