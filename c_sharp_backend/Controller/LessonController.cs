using c_sharp_backend.DTO;
using c_sharp_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace c_sharp_backend.Controller;

[ApiController]
[Route("/api/v1/[controller]")]
public class LessonController: ControllerBase
{
    private readonly LessonService _lessonService;

    public LessonController(LessonService lessonService)
    {
        _lessonService = lessonService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLesson(int id)
    {
        try
        {
            var lesson = await _lessonService.GetLessonById(id);
            if (lesson == null)
            {
                return NotFound(new { message = "Lesson not found." });
            }

            return Ok(lesson);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllLessons()
    {
        try
        {
            var lessons = await _lessonService.GetAllLessons();
            return Ok(lessons);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("add")]
    public async Task<IActionResult> CreateLesson([FromBody] LessonDto lessonDto)
    {
        try
        {
            var createdLesson = await _lessonService.AddLesson(lessonDto);
            return CreatedAtAction(nameof(GetLesson), new { id = createdLesson.LessonName }, createdLesson);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateLesson(int id, [FromBody] LessonDto lessonDto)
    {
        try
        {
            var existingLesson = await _lessonService.GetLessonById(id);
            if (existingLesson == null)
            {
                return NotFound(new { message = "Lesson not found." });
            }

            if (existingLesson.LessonName == lessonDto.LessonName)
            {
                return BadRequest(new { message = "The lesson name is already the same as the current one." });
            }

            var updatedLesson = await _lessonService.UpdateLesson(lessonDto);
            return Ok(updatedLesson);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteLesson(int id)
    {
        try
        {
            await _lessonService.DeleteLesson(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}