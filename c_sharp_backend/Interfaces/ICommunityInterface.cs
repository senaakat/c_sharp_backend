using c_sharp_backend.DTOs;

namespace c_sharp_backend.Interfaces;

public interface ICommunityInterface
{
    Task<IEnumerable<CommunityDto>> GetAllCommunities();
    
    Task<CommunityDto?> GetaCommunity(String shortName);
    
    Task<CommunityDto?> GetCommunityByID(int id);
    
    Task<CommunityDto?> AddCommunity(CommunityDto communityDto);
    
    Task<CommunityDto?> UpdateCommunity(CommunityDto communityDto, int id);

    Task DeleteCommunity(int id);
}