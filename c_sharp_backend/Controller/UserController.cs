using c_sharp_backend.DTO;
using c_sharp_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace c_sharp_backend.Controller;

[ApiController]
[Route("/api/v1/[controller]")]
public class UserController: ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetUser(string email)
    {
        try
        {
            var user = await _userService.GetUserOne(email);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        try
        {
            var existingUser = await _userService.GetUserOne(userDto.email);
            if (existingUser == null)
            {
                var user = await _userService.AddUser(userDto);
                return CreatedAtAction(nameof(GetUser), new { email = user.email }, user);
            }
            return NotFound(new { message = "User already exist." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto,int id )
    {
        try
        {
            var existingUser = await _userService.GetUserId(id);
            if (existingUser == null)
            {
                return NotFound(new { message = "User not found." });
            }
            
            if (existingUser.email == userDto.email)
            {
                return BadRequest(new { message = "The email is already exist with this user." });
            }

            var updatedUser = await _userService.UpdateUser(userDto, id);
            if (updatedUser == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(updatedUser);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var existingUser = await _userService.GetUserId(id);
            if (existingUser == null)
            {
                return NotFound(new { message = "User not found." });
            }
            
            await _userService.DeleteUser(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}