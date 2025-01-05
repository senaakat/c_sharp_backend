using c_sharp_backend.Config;
using c_sharp_backend.Models;
using c_sharp.Models;
using Microsoft.EntityFrameworkCore;

namespace c_sharp_backend.Repository;

public class ChatterCraftRepository
{
    private readonly AppDbContext _appDbContext;

    public ChatterCraftRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        
    }

    public async Task<IEnumerable<ChatterCraft>> GetAllChatsAsync() =>
        await _appDbContext.Set<ChatterCraft>().ToListAsync();
    
    public async Task<ChatterCraft> GetChatAsync(int id) =>
        await _appDbContext.Set<ChatterCraft>().
            FirstOrDefaultAsync(x=>x.Id == id); 
    

    

    public async Task<ChatterCraft> AddChatAsync(ChatterCraft chatterCraft)
    {
        await _appDbContext.Set<ChatterCraft>().AddAsync(chatterCraft);
        await _appDbContext.SaveChangesAsync();
        return chatterCraft;

    }
    public async Task<ChatterCraft> UpdateChatAsync(ChatterCraft chatterCraft)
    {
        _appDbContext.Set<ChatterCraft>().Update(chatterCraft);
        await _appDbContext.SaveChangesAsync();
        return chatterCraft;
    }

    public async Task DeleteChatAsync(int id )
    {
        var chat = await GetChatAsync(id);
        if (chat != null)
        {
            _appDbContext.Set<ChatterCraft>().Remove(chat);
            await _appDbContext.SaveChangesAsync();
        }
    }

}