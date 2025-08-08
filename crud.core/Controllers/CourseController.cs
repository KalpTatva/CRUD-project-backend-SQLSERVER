using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using crud.repository.Models;
using Microsoft.AspNetCore.Mvc;
using webapi.Repository.ViewModels;
using webapi.Service.Interfaces;

namespace webapiproject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;
    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [Route("[action]")]
    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        try
        {
            string? userId = HttpContext.Items["UserId"]?.ToString();

            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid or missing token" });
            }

            List<Course> courses = await _courseService.GetAllCourses();
            if (courses.Any())
            {
                return Ok(new { message = "courses found successfully!", data = courses });
            }
            return NotFound(new { message = "No courses found!" });
        }
        catch (Exception e)
        {
            return StatusCode(500, $"{e.Message}");
        }
    }

    [Route("[action]")]
    [HttpGet]
    public IActionResult GetCourseById(int id)
    {
        try
        {
            string? userId = HttpContext.Items["UserId"]?.ToString();

            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid or missing token" });
            }
            Course? course = _courseService.GetCourseById(id);
            if (course != null)
            {
                return Ok(new { message = "courses found successfully!", data = course });
            }
            return NotFound(new { message = "couse not found!" });
        }
        catch (Exception e)
        {
            return StatusCode(500, $"{e.Message}");
        }
    }

    [Route("[action]")]
    [HttpPost]
    public IActionResult AddCourse([FromBody] CourseViewModel addCourseViewModel)
    {
        try
        {
            string? userId = HttpContext.Items["UserId"]?.ToString();

            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid or missing token" });
            }
            if (ModelState.IsValid)
            {
                ResponseViewModel response = _courseService.AddCourse(addCourseViewModel);
                if (response.success)
                {
                    return Ok(new { success = true, message = $"{response.message}" });
                }
                return BadRequest(new { success = false, message = $"{response.message}" });
            }
            else
            {
                return BadRequest(new { success = false, message = $"model state is invalid, error count :  ${ModelState.ErrorCount}" });
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Error occured while adding course! {e.Message}");
        }
    }

    [Route("[action]")]
    [HttpPut]
    public IActionResult EditCourse([FromBody] CourseViewModel editCourseViewModel)
    {
        try
        {
            string? userId = HttpContext.Items["UserId"]?.ToString();

            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid or missing token" });
            }
            if (ModelState.IsValid)
            {
                ResponseViewModel response = _courseService.EditCourse(editCourseViewModel);
                if (response.success)
                {
                    return Ok(new { success = true, message = $"{response.message}" });
                }
                return BadRequest(new { success = false, message = $"{response.message}" });
            }
            else
            {
                return BadRequest(new { success = false, message = $"model state is invalid, error count : ${ModelState.ErrorCount}" });
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Error occured while editing course! {e.Message}");
        }
    }

    [Route("[action]")]
    [HttpDelete]
    public IActionResult DeleteCourse(int id)
    {
        try
        {
            string? userId = HttpContext.Items["UserId"]?.ToString();

            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid or missing token" });
            }
            ResponseViewModel? res = _courseService.DeleteCourseById(id);
            if (res.success)
            {
                return Ok(new { message = "Course Deleted successfully!"});
            }
            return NotFound(new { message = "couse not found!" });
        }
        catch (Exception e)
        {
            return StatusCode(500, $"{e.Message}");
        }
    }

}
