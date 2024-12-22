using c_sharp_backend.DTO;
using c_sharp_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace c_sharp_backend.Controller;

[ApiController]
[Route("/api/v1/[controller]")]
public class LessonPdfController : ControllerBase
{
    private readonly LessonPdfService _lessonPdfService;

    public LessonPdfController(LessonPdfService lessonPdfService)
    {
        _lessonPdfService = lessonPdfService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPdf(int id)
    {
        try
        {
            var pdf = await _lessonPdfService.GetPdfById(id);
            if (pdf == null)
            {
                return NotFound(new { message = "PDF not found." });
            }

            return Ok(pdf);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllPdfs()
    {
        try
        {
            var pdfs = await _lessonPdfService.GetAllPdfs();
            return Ok(pdfs);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("add")]
    public async Task<IActionResult> CreatePdf([FromBody] LessonPdfDto lessonPdfDto)
    {
        try
        {
            var createdPdf = await _lessonPdfService.AddPdf(lessonPdfDto.pdfName!, lessonPdfDto);
            return CreatedAtAction(nameof(GetPdf), new { id = createdPdf.pdfName }, createdPdf);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdatePdf(int id, [FromBody] LessonPdfDto lessonPdfDto)
    {
        try
        {
            var existingPdf = await _lessonPdfService.GetPdfById(id);
            if (existingPdf == null)
            {
                return NotFound(new { message = "PDF not found." });
            }

            if (existingPdf.pdfName == lessonPdfDto.pdfName)
            {
                return BadRequest(new { message = "The PDF name is already the same as the current one." });
            }

            var updatedPdf = await _lessonPdfService.UpdatePdf(lessonPdfDto);
            return Ok(updatedPdf);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeletePdf(int id)
    {
        try
        {
            await _lessonPdfService.DeletePdf(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
}