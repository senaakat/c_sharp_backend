using c_sharp_backend.DTO;
using c_sharp_backend.Models;

namespace c_sharp_backend.Interfaces;

public interface ILikeInterface
{
    Task<IEnumerable<LikeDto>> GetAllLikes(int id,LikeType likeType);
    
    Task<LikeDto?> GetLike(int id);
    
    Task<LikeDto?> AddLike(LikeDto likeDto);
    
    Task DeleteLike(int id);
}