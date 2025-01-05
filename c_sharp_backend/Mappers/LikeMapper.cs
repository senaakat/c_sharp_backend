using c_sharp_backend.DTO;
using c_sharp_backend.Models;
using c_sharp_backend.Repository;

namespace c_sharp_backend.Mappers;

public class LikeMapper
{
    private readonly UserRepository _userRepository;
    public LikeMapper(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public static LikeDto MapLikeToLikeDto(Like like)
    {
        if(like==null) return null;
        return new LikeDto
        {
            UserId = like.UserId,
            Type = like.Type,
            ContentId = like.ContentId,
        };
    }

    public  async Task<Like?> MapLikeDtoToLike(LikeDto likeDto)
    {
        if (likeDto == null) return null;
        return new Like
        {
            UserId = likeDto.UserId,
            Type = likeDto.Type,
            ContentId = likeDto.ContentId,
        };
    }
}