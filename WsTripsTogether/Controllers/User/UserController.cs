using Microsoft.AspNetCore.Mvc;
using WsTripsTogether.Dto.User;
using WsTripsTogether.Exceptions;
using WsTripsTogether.Services.User;

namespace WsTripsTogether.Controllers.User;

[Route("/api/user")]
[ApiController]
public class UserController(IUserService userService) : BaseController
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] UserDto userDto) => 
        Ok(await userService.AddAsync(userDto));

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDto loginDto)
    {
        if (string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password))
            return StatusCode(400, "Request error");

        try
        {
            return Ok(await userService.LoginAsync(loginDto));
        }
        catch (UserException)
        {
            return StatusCode(400, "Request error");
        }
    }
}