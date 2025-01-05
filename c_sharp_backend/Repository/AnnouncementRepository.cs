using c_sharp_backend.Config;
using c_sharp.Models;
using Microsoft.EntityFrameworkCore;

namespace c_sharp_backend.Repository;

public class AnnouncementRepository

{
    private readonly AppDbContext _appDbContext;
    public AnnouncementRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<Announcement>> GetAllAnnouncementAsync() =>
        await _appDbContext.Set<Announcement>().ToListAsync();
        
    public async Task<Announcement?> GetAnnouncementAsync(int id) =>
        await _appDbContext.Set<Announcement>().FirstOrDefaultAsync(u => u.Id == id);

    public async Task<Announcement> AddAnnoucementAsync(Announcement announcement)
    {
        await _appDbContext.Set<Announcement>().AddAsync(announcement);
        await _appDbContext.SaveChangesAsync();
        return announcement;
    }

    public async Task<Announcement> UpdateAnnoucementAsync(Announcement anouncement)
    {
        _appDbContext.Set<Announcement>().Update(anouncement);
        await _appDbContext.SaveChangesAsync();
        return anouncement;
    }

    public async Task DeleteAnnoucementAsync(int id)
    {
        var announcement = await _appDbContext.Set<Announcement>().FindAsync(id);
        if (announcement != null)
        {
            _appDbContext.Set<Announcement>().Remove(announcement);
            await _appDbContext.SaveChangesAsync();
        }
    }
    
}