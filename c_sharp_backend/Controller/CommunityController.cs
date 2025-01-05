using c_sharp_backend.DTOs;
using c_sharp_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace c_sharp_backend.Controller;

[ApiController]
[Route("/api/v1/[controller]")]
public class CommunityController: ControllerBase
{
    private readonly CommunityService _communityService;

    public CommunityController(CommunityService communityService)
    {
        _communityService= communityService;
    }

    [HttpGet("{shortName}")]
    public async Task<IActionResult> GetCommunity(String shortName)
    {
        try
        {
            var community = await _communityService.GetaCommunity(shortName);
            if (community == null)
            {
                return NotFound(new { message = "Community not found." });
            }

            return Ok(community);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("")]
    public async Task<IActionResult> GetCommunities()
    {
        try
        {
            var communities = await _communityService.GetAllCommunities();
            return Ok(communities);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> CreateCommunity([FromBody] CommunityDto communityDto)
    {
        try
        {
            var existingCommunity = await _communityService.GetaCommunity(communityDto.ShortName);
            if (existingCommunity == null)
            {
                var community = await _communityService.AddCommunity(communityDto);
                return CreatedAtAction(nameof(GetCommunity), new { shortName = community.ShortName }, community);
            }
            return NotFound(new { message = "This community already exist." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateCommunity([FromBody] CommunityDto communityDto,int id )
    {
        try
        {
            var existingCommunity = await _communityService.GetCommunityByID(id);
            if (existingCommunity == null)
            {
                return NotFound(new { message = "Community not found." });
            }
            var existingCommunitywithshortName = await _communityService.GetaCommunity(communityDto.ShortName);
            //TODO
            //buraya bütün comminnityleri alıp içinden bizimkisinin idsi olmayanların shortnamelerine bakcaksım
            if (existingCommunitywithshortName != null && existingCommunitywithshortName.ShortName != existingCommunity.ShortName)
            {
                return BadRequest(new { message = "This Community's shortName already exists." });
            }


            var updatedCommunity = await _communityService.UpdateCommunity(communityDto, id);
            if (updatedCommunity == null)
            {
                return NotFound(new { message = "Community not found." });
            }

            return Ok(updatedCommunity);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteCommunity(int id)
    {
        try
        {
            var existingCommunity = await _communityService.GetCommunityByID(id);
            if (existingCommunity == null)
            {
                return NotFound(new { message = "Community not found." });
            }
            
            await _communityService.DeleteCommunity(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}