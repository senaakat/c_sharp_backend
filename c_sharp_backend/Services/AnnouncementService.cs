using c_sharp_backend.Config;
using c_sharp_backend.Interfaces;
using c_sharp_backend.Models;
using c_sharp_backend.Repository;
using c_sharp.Models;

namespace c_sharp_backend.Services;

public class AnnouncementService:IAnnoucementInterface
{
    private readonly AnnouncementRepository _announcementRepository;
    private readonly LikeRepository _likeRepository;
    private readonly AppDbContext _dbContext;

    public AnnouncementService(AppDbContext dbContext, AnnouncementRepository announcementRepository, LikeRepository likeRepository)
    {
        _dbContext = dbContext;
        _announcementRepository = announcementRepository;
        _likeRepository = likeRepository;

    }

    public async Task<Announcement?> GetAnnouncement(int id)
    {
        try
        {
            var announcement = await _announcementRepository.GetAnnouncementAsync(id);
            if (announcement == null) return null;

            return announcement;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while retrieving annnouncement with announcement {id}.", ex);
        }
    }

   

    public async Task<IEnumerable<Announcement>> GetAllAnnouncements()
    {
        try
        {
            var announcements = await _announcementRepository.GetAllAnnouncementAsync();
            return announcements;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving announcements.", ex);
        }
    }


    public async Task<Announcement?> AddAnnouncement(Announcement announcement)
    {
        try
        {
            var announcementAsync = await _announcementRepository.GetAnnouncementAsync(announcement.Id);
            if (announcementAsync != null)
            {
                throw new Exception($"This announcement already exists: {announcementAsync}");
            }

            var addedAnnouncement = await _announcementRepository.AddAnnoucementAsync(announcement);
            return addedAnnouncement;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}, Details: {ex.InnerException?.Message}");
            throw new Exception("An error occurred while adding the announcement.", ex);
        }

    }


    public async Task<Announcement?> UpdateAnnouncement(Announcement announcement, int id)
    {
        try
        {
            var existingAnnouncement = await _announcementRepository.GetAnnouncementAsync(id);
            if (existingAnnouncement == null)
            {
                throw new Exception("Announcement not found.");
            }
            var likeCount = await _likeRepository.GetCountsforAnnouncementAsync(id);


            existingAnnouncement.Title = announcement.Title;
            existingAnnouncement.Image = announcement.Image;
            existingAnnouncement.Likes = likeCount;
            existingAnnouncement.Text = announcement.Text;

            var updatedAnnouncement = await _announcementRepository.UpdateAnnoucementAsync(existingAnnouncement);

            return updatedAnnouncement;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while updating announcement with id {id}.", ex);
        }
    }

    public async Task DeleteAnnouncement(int id)
    {
        try
        {
            var announcement = await _announcementRepository.GetAnnouncementAsync(id);
            if (announcement == null)
            {
                throw new Exception("announcement not found.");
            }

            await _announcementRepository.DeleteAnnoucementAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while deleting announcement with id {id}.", ex);
        }
    }
}