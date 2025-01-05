using c_sharp_backend.Config;
using c_sharp_backend.Models;
using c_sharp.Models;
using Microsoft.EntityFrameworkCore;

namespace c_sharp_backend.Repository;

public class LikeRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly AnnouncementRepository _announcementRepository;
    private readonly ChatterCraftRepository _chatterCraftRepository;

    public LikeRepository(AppDbContext appDbContex,AnnouncementRepository announcementRepository, ChatterCraftRepository chatterCraftRepository)
    {
        _appDbContext = appDbContex;
        _announcementRepository = announcementRepository;
        _chatterCraftRepository = chatterCraftRepository;
        
    }

    public async Task<IEnumerable<Like>> GetAllLikesOnContentAsync(int contentId, LikeType likeType) =>
          await _appDbContext.Set<Like>().Include(x => x.user)
            .Where(like => like.ContentId == contentId && like.Type == likeType).ToListAsync();
    
    public async Task<Like> GetLikedByUser(int contentId,LikeType likeType,int userId) =>
        await _appDbContext.Set<Like>().
            Include(x => x.user).
            FirstOrDefaultAsync(x=>x.UserId ==userId && x.ContentId==contentId && x.Type==likeType); 
    public async Task<Like> GetLikeByIdAsync(int id) =>
        await _appDbContext.Set<Like>().
            Include(x => x.user).
            FirstOrDefaultAsync(x=>x.Id == id);

    public async Task<int> GetCountsforAnnouncementAsync(int contentId)
    {
        var likes = await GetAllLikesOnContentAsync(contentId, LikeType.Announcement);
        return likes.Count();
    }
    public async Task<int> GetCountsforChatterCraftAsync (int contentId)
    {
        var likes = await GetAllLikesOnContentAsync(contentId, LikeType.ChatterCraft);
        return likes.Count();
    }

    public async Task UpdateLikeAsync(LikeType likeType,int contentId)
    {
        if (likeType == LikeType.Announcement)
        {
         var announcement=await _announcementRepository.GetAnnouncementAsync(contentId);
         if (announcement != null)
         {
             var likeCount = await GetCountsforAnnouncementAsync(announcement.Id);
             announcement.Likes = likeCount;
             _appDbContext.SaveChangesAsync();
         }
        }
        else if (likeType == LikeType.ChatterCraft)
        {
            var chatterCraft = await _chatterCraftRepository.GetChatAsync(contentId);
            if (chatterCraft != null)
            {
                var likeCount=await GetCountsforChatterCraftAsync(chatterCraft.Id);
                chatterCraft.Likes=likeCount;
                _appDbContext.SaveChangesAsync();
            }
        }
    }

    public async Task<Like> AddLikeAsync(Like newLike)
    {
        await _appDbContext.Set<Like>().AddAsync(newLike);
        await _appDbContext.SaveChangesAsync();
        await UpdateLikeAsync(newLike.Type,newLike.ContentId );

        return newLike;
    }

    public async Task DeleteLikeAsync(int id )
    {
        var like = await GetLikeByIdAsync(id);
        if (like != null)
        {
            _appDbContext.Set<Like>().Remove(like);
            await _appDbContext.SaveChangesAsync();
        }
    }

}