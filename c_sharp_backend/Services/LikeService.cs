using c_sharp_backend.Config;
using c_sharp_backend.DTO;
using c_sharp_backend.Interfaces;
using c_sharp_backend.Mappers;
using c_sharp_backend.Models;
using c_sharp_backend.Repository;

namespace c_sharp_backend.Services;

public class LikeService:ILikeInterface
{
    private readonly LikeMapper _likeMapper;
    private readonly LikeRepository _likeRepository;
    private readonly AppDbContext _appDbContext;

    public LikeService(LikeRepository likeRepository, LikeMapper likeMapper, AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _likeRepository = likeRepository;
        _likeMapper = likeMapper;
    }
    
   
    
    public async Task<IEnumerable<LikeDto>> GetAllLikes(int contentId,LikeType likeType)
    {
        var likes = await _likeRepository.GetAllLikesOnContentAsync(contentId,likeType);
        return likes.Select(LikeMapper.MapLikeToLikeDto);
    }
    
    public async Task<LikeDto?> GetLike(int id)
    {
        var like = await _likeRepository.GetLikeByIdAsync(id);
        return like == null ? null : LikeMapper.MapLikeToLikeDto(like);
    }

    public async Task<LikeDto?> AddLike(LikeDto likeDto)
    {
        var like = await _likeMapper.MapLikeDtoToLike(likeDto);
        var existingLike=await _likeRepository.GetLikedByUser(like.ContentId,like.Type,like.UserId);
        if(existingLike!=null) throw new Exception($"Like is already exists. Can't like more than one");

        await _likeRepository.AddLikeAsync(like);
        return LikeMapper.MapLikeToLikeDto(like);
    }

  

    public async Task DeleteLike(int id)
    {
        await _likeRepository.DeleteLikeAsync(id);
    }
}