using c_sharp_backend.DTO;
using c_sharp_backend.Models;
using c_sharp_backend.Services;
using c_sharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace c_sharp_backend.Controller;

[ApiController]
[Route("/api/v1/[controller]")]
public class ChatterCraftController:ControllerBase
{
    private readonly LikeService _likeService;
    private readonly UserService _userService;
    private readonly ChatterCraftService _chatterCraftService;

    public ChatterCraftController(LikeService likeService, UserService userService,ChatterCraftService chatterCraftService)
    {
        _chatterCraftService=chatterCraftService;
       _likeService=likeService;
       _userService=userService;
    }
    
    
    [HttpGet("{id}")]
        public async Task<IActionResult> GetChatterCraft(int id)
        {
            try
            {
                var chatterCraft = await _chatterCraftService.GetChatterCraft(id);
                if (chatterCraft == null)
                {
                    return NotFound(new { message = "Chat not found." });
                }
                return Ok(chatterCraft);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpGet("all")]
        public async Task<IActionResult> GetAllChatterCrafts(int id)
        {
            try
            {
                var chatterCrafts = await _chatterCraftService.GetAllChatterCrafts();
                return Ok(chatterCrafts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> AddChatterCraft([FromBody]  ChatterCraft chatterCraft)
        {
            try
            {
                 await _chatterCraftService.AddChatterCraft(chatterCraft);
                 return Ok(new {message= "Chattercraft added successfully"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateChatterCraft(int id, [FromBody] ChatterCraft chatterCraft)
        {
            try
            {
                var updatedChatterCraft = await _chatterCraftService.UpdateChatterCraft(chatterCraft, id);
                if (updatedChatterCraft == null)
                {
                    return NotFound(new { message = "Chat not found." });
                }
                return Ok(updatedChatterCraft);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        
        
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteChatterCraft(int id)
        {
            try
            {
                await _chatterCraftService.DeleteChatterCraft(id);
                return Ok(new { message = "Chat has been deleted." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
}