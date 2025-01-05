using c_sharp_backend.Config;
using c_sharp.Models;
using Microsoft.EntityFrameworkCore;


namespace c_sharp_backend.Repository;


public class GossipRepository
{
    private readonly AppDbContext _appDbContext;

    public GossipRepository(AppDbContext appDbContext) 
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<Gossip>> GetAllGossips() =>
        await _appDbContext.Set<Gossip>().ToListAsync();
    
    public async Task<Gossip> GetGossipByIdAsync(int id) =>
        await _appDbContext.Set<Gossip>().FirstOrDefaultAsync(x=>x.Id == id); 

    public async Task<Gossip> AddGossipAsync(Gossip newGossip)
    {
        await _appDbContext.Set<Gossip>().AddAsync(newGossip);
        await _appDbContext.SaveChangesAsync();
        return newGossip;
    }

    public async Task<Gossip?> UpdateGossipAsync(Gossip updatedGossip )
    {
        _appDbContext.Set<Gossip>().Update(updatedGossip);
        await _appDbContext.SaveChangesAsync();
        return updatedGossip;
    }

    public async Task DeleteGossipAsync(int id )
    {
        var gossip = await GetGossipByIdAsync(id);
        if (gossip != null)
        {
            _appDbContext.Set<Gossip>().Remove(gossip);
            await _appDbContext.SaveChangesAsync();
        }
    }
}