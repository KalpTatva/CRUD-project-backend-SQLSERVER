using System.Threading.Tasks;
using crud.repository.Models;
using webapi.Repository.Interfaces;
using webapi.Repository.ViewModels;
using webapi.Service.Interfaces;

namespace webapi.Service.Implementations;

public class CourseService : ICourseService
{

    private ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<List<Course>> GetAllCourses()
    {
        try
        {
            return await _courseRepository.GetAllCourses();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Course? GetCourseById(int id)
    {
        try
        {
            return _courseRepository.GetCourseById(id);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public ResponseViewModel AddCourse(CourseViewModel courseViewModel)
    {
        try
        {
            if (courseViewModel == null)
            {
                return new ResponseViewModel
                {
                    success = false,
                    message = "Course is empty!"
                };
            }

            Course course = new Course()
            {
                CourseName = courseViewModel.CourseName.Trim(),
                CourseContent = courseViewModel.CourseContent.Trim(),
                Credits = courseViewModel.Credits,
                Department = courseViewModel.Department.Trim(),
                CreatedById = courseViewModel.UserId,
            };

            _courseRepository.AddCourse(course);
            return new ResponseViewModel
            {
                success = true,
                message = "New course added successfully!"
            };
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public ResponseViewModel EditCourse(CourseViewModel courseViewModel)
    {
        try
        {
            if (courseViewModel == null)
            {
                return new ResponseViewModel
                {
                    success = false,
                    message = "Course is empty!"
                };
            }
            Course? course = _courseRepository.GetCourseById(courseViewModel.CourseId);
            if (course == null)
            {
                return new ResponseViewModel
                {
                    success = false,
                    message = "Course not found!"
                };
            }

            course.CourseName = courseViewModel.CourseName.Trim();
            course.CourseContent = courseViewModel.CourseContent.Trim();
            course.Credits = courseViewModel.Credits;
            course.Department = courseViewModel.Department.Trim();
            course.EditedById = courseViewModel.UserId;
            course.EditedAt = DateTime.Now;

            _courseRepository.EditCourse(course);

            return new ResponseViewModel
            {
                success = true,
                message = "New course added successfully!"
            };
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }


    public ResponseViewModel DeleteCourseById(int id)
    {
        try
        {
            Course? course = _courseRepository.GetCourseById(id);
            if (course != null)
            {

                _courseRepository.DeleteCourse(id, 2);
                return new ResponseViewModel
                {
                    success = true,
                    message = "Course deleted successfully!"
                };
            }
            throw new Exception($"Course not found!");
        }
        catch (Exception e)
        {
            return new ResponseViewModel
            {
                success = false,
                message = e.Message
            };
        }
    }
    
}
