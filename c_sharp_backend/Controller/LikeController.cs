using c_sharp_backend.DTO;
using c_sharp_backend.Models;
using c_sharp_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace c_sharp_backend.Controller;

[ApiController]
[Route("/api/v1/[controller]")]
public class LikeController:ControllerBase
{
    private readonly LikeService _likeService;

    public LikeController(LikeService likeService)
    {
       _likeService=likeService;
    }
    
    
    [HttpGet("{id}")]
        public async Task<IActionResult> GetLikeByID(int id)
        {
            try
            {
                var like = await _likeService.GetLike(id);
                if (like == null)
                {
                    return NotFound(new { message = "Like not found." });
                }
                return Ok(like);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpGet("all")]
        public async Task<IActionResult> GetAllLikesforContent(int contentId, LikeType likeType)
        {
            try
            {
                var likes = await _likeService.GetAllLikes(contentId, likeType);
                return Ok(likes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> AddLike([FromBody] LikeDto likeDto)
        {
            try
            {
                 await _likeService.AddLike(likeDto);
                 return Ok(new {message= "Like added successfully"});
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
                await _likeService.DeleteLike(id);
                return Ok(new { message = "Unliked." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
}