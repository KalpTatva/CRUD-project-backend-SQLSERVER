using crud.repository.ViewModels;
using crud.service.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using webapi.Repository.ViewModels;

namespace crud.core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;

    }


    [Route("[action]")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequestViewModel request)
    {
        try
        {
            if (request == null)
            {
                return BadRequest("Invalid Login Credentials");
            }
            ResponseViewModel response = await _userService.VarifyUser(request);
            if (response.success)
            {
                AuthResultViewModel tokens = await _authService.GenerateToken(request);
                return Ok(tokens);
            }
            return Unauthorized("Invalid User Credentials!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
