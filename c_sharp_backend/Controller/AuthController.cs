using c_sharp_backend.DTO;
using c_sharp_backend.Models;
using c_sharp_backend.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace c_sharp_backend.Controller;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var token = await _authService.Login(loginDto.Email, loginDto.Password);

        if (token == null)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        return Ok(new { token });
    }
    
    [HttpPost("logout")]
    public IActionResult LogOut()
    {
        Response.Cookies.Delete("your-jwt-cookie-name");
        return Ok(new { message = "User logged out successfully" });
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto userDto)
    {
        var result = await _authService.Register(userDto);

        if (!result)
        {
            return BadRequest(new { message = "Registration failed. User may already exist." });
        }

        return Ok(new { message = "User registered successfully." });
    }
}
