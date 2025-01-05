using c_sharp_backend.Services;
using c_sharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace c_sharp_backend.Controller;

[ApiController]
[Route("/api/v1/[controller]")]
public class GossipController: ControllerBase
{
    private readonly GossipService _gossipService;

    public GossipController(GossipService gossipService)
    {
        _gossipService= gossipService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGossipById(int id)
    {
        var gossip = await _gossipService.GetGossipByID(id);
        if (gossip == null)
        {
            return NotFound("Gossip cannot be found.");
        }

        return Ok(gossip);
    }
    [HttpGet("")]
    public async Task<IActionResult> GetAllGossips()
    {
        try
        {
            var gossips = await _gossipService.GetAllGossips();
            return Ok(gossips);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> CreateGossip([FromBody] Gossip gossip)
    {
        try
        {
            var createdGossip = await _gossipService.AddGossip(gossip);
            return CreatedAtAction(nameof(GetGossipById), new { id = createdGossip?.Id }, createdGossip);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

   
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteGossip(int id)
    {
        try
        {
            var existingGossip = await _gossipService.GetGossipByID(id);
            if (existingGossip == null)
            {
                return NotFound(new { message = "Community not found." });
            }
            
            await _gossipService.DeleteGossip(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}