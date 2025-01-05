using c_sharp_backend.Config;
using c_sharp_backend.DTOs;
using c_sharp_backend.Interfaces;
using c_sharp_backend.Mappers;
using c_sharp_backend.Repository;

namespace c_sharp_backend.Services;

public class CommunityService :ICommunityInterface
{
    private readonly CommunityRepository _communityRepository;
    private readonly CommunityMapper _communityMapper;
    private readonly AppDbContext _dbContext;

    public CommunityService(AppDbContext dbContext,CommunityRepository communityRepository, CommunityMapper communityMapper)
    {
        _communityRepository = communityRepository;
        _communityMapper = communityMapper;
        _dbContext = dbContext;
    }

    public async Task<CommunityDto?> GetaCommunity (String shortName)
    {
        try
        {
            var community = await _communityRepository.GetCommunityByShortNameAsync(shortName);
            if (community == null) return null;

            return CommunityMapper.MapCommunitytoCommunityDto(community);
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception($"An error occurred while retrieving community with shortName{shortName}.", ex);
        }
    }
    
    public async Task<CommunityDto?> GetCommunityByID (int id)
    {
        try
        {
            var community = await _communityRepository.GetCommunityByIdAsync(id);
            if (community == null) return null;

            return CommunityMapper.MapCommunitytoCommunityDto(community);
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception($"An error occurred while retrieving community with id {id}.", ex);
        }
    }

    public async Task<IEnumerable<CommunityDto>> GetAllCommunities()
    {
        try
        {
            var communities = await _communityRepository.GetAllCommunitiesAsync();
            return  communities.Select(community => CommunityMapper.MapCommunitytoCommunityDto(community));
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception("An error occurred while retrieving communities.", ex);
        }
    }


    public async Task<CommunityDto?> AddCommunity(CommunityDto communityDto)
    {
        try
        {
            var existingCommunity = await _communityRepository.GetCommunityByShortNameAsync(communityDto.ShortName);
            if (existingCommunity != null)
            {
                throw new Exception($"Community with short name {communityDto.ShortName} already exists.");
            }
            var community = _communityMapper.MapCommunityDtotoCommunity(communityDto);
            var addedCommunity = await _communityRepository.AddCommunityAsync(community);
            return CommunityMapper.MapCommunitytoCommunityDto(addedCommunity);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}, Details: {ex.InnerException?.Message}");
            throw new Exception("An error occurred while adding the community.", ex);
        }
        
    }

    public async Task<CommunityDto?> UpdateCommunity(CommunityDto communityDto, int id)
    {
        try
        {
            var existingCommunity = await _communityRepository.GetCommunityByIdAsync(id);
            if (existingCommunity == null)
            {
                throw new Exception("Community not found.");
            }
            var community = _communityMapper.MapCommunityDtotoCommunity(communityDto);
            existingCommunity.CommunityName = community.CommunityName;
            existingCommunity.Image = community.Image;
            existingCommunity.ShortName = community.ShortName;
            existingCommunity.Text = community.Text;
            
            var updatedCommunity = await _communityRepository.UpdateCommunityAsync(existingCommunity);
            
            return CommunityMapper.MapCommunitytoCommunityDto(updatedCommunity);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while updating community with id {id}.", ex);
        }
    }

    public async Task DeleteCommunity(int id)
    {
        try
        {
            var community = await _communityRepository.GetCommunityByIdAsync(id);
            if (community == null)
            {
                throw new Exception("community not found.");
            }

            await _communityRepository.DeleteCommunityAsync(id);
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception($"An error occurred while deleting community with id {id}.", ex);
        }
    }
}