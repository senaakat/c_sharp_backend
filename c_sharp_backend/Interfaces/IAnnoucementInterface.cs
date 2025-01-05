using c_sharp.Models;

namespace c_sharp_backend.Interfaces;

public interface IAnnoucementInterface
{
    Task<IEnumerable<Announcement>> GetAllAnnouncements();
    
    Task<Announcement?> GetAnnouncement(int id);
    
    Task<Announcement?> AddAnnouncement(Announcement announcement);
    
    Task<Announcement?> UpdateAnnouncement(Announcement announcement, int id);

    Task DeleteAnnouncement(int id);
}