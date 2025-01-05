
using c_sharp_backend.Services;
using c_sharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace c_sharp_backend.Controller;

[ApiController]
[Route("/api/v1/[controller]")]
public class AnnouncementController:ControllerBase
{
    private readonly AnnouncementService _announcementService;
    private readonly LikeService _likeService;
    

    public AnnouncementController(AnnouncementService announcementService,LikeService likeService)
    {
        _announcementService = announcementService;
        _likeService = likeService;
    }
    
    
    
    [HttpGet("{id}")]
        public async Task<IActionResult> GetAnnouncement(int id)
        {
            try
            {
                var announcement = await _announcementService.GetAnnouncement(id);
                if (announcement == null)
                {
                    return NotFound(new { message = "Announcement not found." });
                }
                return Ok(announcement);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAnnouncements()
        {
            try
            {
                var announcements = await _announcementService.GetAllAnnouncements();
                return Ok(announcements);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("add")]
        public async Task<IActionResult> CreateAnnoucement([FromBody]  Announcement announcement)
        {
            try
            {
                var existingAnnoucement = await _announcementService.GetAnnouncement(announcement.Id);
                if (existingAnnoucement == null)
                {
                    var addedAnnouncement = await _announcementService.AddAnnouncement(announcement);
                    return CreatedAtAction(nameof(GetAnnouncement), new { id=addedAnnouncement.Id }, addedAnnouncement);
                }
                return NotFound(new { message = "This announcement already exist." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAnnouncement(int id, [FromBody] Announcement announcement)
        {
            try
            {
                var updatedAnnouncement = await _announcementService.UpdateAnnouncement(announcement, id);
                if (updatedAnnouncement == null)
                {
                    return NotFound(new { message = "Announcement not found." });
                }
                return Ok(updatedAnnouncement);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            try
            {
                await _announcementService.DeleteAnnouncement(id);
                return Ok(new { message = "Annoucement deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
}