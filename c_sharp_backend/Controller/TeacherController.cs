using c_sharp_backend.DTO;
using c_sharp_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace c_sharp_backend.Controller;

[ApiController]
[Route("/api/v1/[controller]")]
public class TeacherController:ControllerBase
{
    private readonly TeacherService _teacherService;

    public TeacherController(TeacherService teacherService)
    {
        _teacherService = teacherService;
    }
    
    [HttpPut("changeRoleToTeacher/{userId}")]
    public async Task<IActionResult> ChangeRoleToTeacher(int userId)
    {
        try
        {
            await _teacherService.ChangeUserRoleToTeacherAsync(userId);
            return Ok("User role successfully changed to Teacher.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            try
            {
                var teacher = await _teacherService.GetTeacherId(id);
                if (teacher == null)
                {
                    return NotFound(new { message = "Teacher not found." });
                }
                return Ok(teacher);
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
                var users = await _teacherService.GetAllTeachers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> AddTeacher([FromBody] TeacherDto teacherDto)
        {
            try
            {
                var createdTeacher = await _teacherService.AddTeacher(teacherDto);
                return CreatedAtAction(nameof(GetTeacherById), new { id = createdTeacher.UserId }, createdTeacher);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody] TeacherDto teacherDto)
        {
            try
            {
                var updatedTeacher = await _teacherService.UpdateTeacher(teacherDto, id);
                if (updatedTeacher == null)
                {
                    return NotFound(new { message = "Teacher not found." });
                }
                return Ok(updatedTeacher);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                await _teacherService.DeleteTeacher(id);
                return Ok(new { message = "Teacher deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
}