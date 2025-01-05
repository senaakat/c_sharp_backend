using AutoMapper;
using c_sharp_backend.DTOs;
using c_sharp.Models;

namespace c_sharp_backend.Mappers;

public class CommunityMapper:Profile
{
    public static CommunityDto MapCommunitytoCommunityDto(Community community)
    {
        if (community == null) return null;
    
        return new CommunityDto
        { 
            CommunityName =community.CommunityName,
                Image=community.Image,
                ShortName=community.ShortName,
                Text=community.Text
                
            
        };
    }
   
    public Community MapCommunityDtotoCommunity(CommunityDto communityDto)
    {
        if (communityDto == null) return null;
    
        return new Community
        {
            CommunityName = communityDto.CommunityName,
            Image=communityDto.Image,
            ShortName=communityDto.ShortName,
            Text=communityDto.Text
        };
    }
    
}