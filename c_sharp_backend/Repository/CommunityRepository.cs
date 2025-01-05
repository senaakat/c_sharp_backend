using c_sharp_backend.Config;
using c_sharp.Models;
using Microsoft.EntityFrameworkCore;


namespace c_sharp_backend.Repository;


public class CommunityRepository
{
    private readonly AppDbContext _appDbContext;

    public CommunityRepository(AppDbContext appDbContext) 
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<Community>> GetAllCommunitiesAsync() =>
        await _appDbContext.Set<Community>().ToListAsync();
    
    public async Task<Community> GetCommunityByIdAsync(int id) =>
    await _appDbContext.Set<Community>().FirstOrDefaultAsync(x=>x.Id == id); 
    
    public async Task<Community> GetCommunityByShortNameAsync(String shortName) =>
    await _appDbContext.Set<Community>().FirstOrDefaultAsync(x=>x.ShortName == shortName);

    public async Task<Community> AddCommunityAsync(Community newCommunity)
    {
         await _appDbContext.Set<Community>().AddAsync(newCommunity);
        await _appDbContext.SaveChangesAsync();
         return newCommunity;
    }

    public async Task<Community> UpdateCommunityAsync(Community community)
    {
         _appDbContext.Set<Community>().Update(community);
        await _appDbContext.SaveChangesAsync();
        return community;
    }

    public async Task DeleteCommunityAsync(int id )
    {
        var community = await GetCommunityByIdAsync(id);
        if (community != null)
        {
            _appDbContext.Set<Community>().Remove(community);
            await _appDbContext.SaveChangesAsync();
        }
    }


}