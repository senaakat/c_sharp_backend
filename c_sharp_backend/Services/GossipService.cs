using c_sharp_backend.Config;
using c_sharp_backend.Interfaces;
using c_sharp_backend.Repository;
using c_sharp.Models;

namespace c_sharp_backend.Services;

public class GossipService :IGossipInterface
{
    private readonly GossipRepository _gossipRepository;
    private readonly AppDbContext _dbContext;

    public GossipService(AppDbContext dbContext,GossipRepository gossipRepository)
    {
        _gossipRepository = gossipRepository;
        _dbContext = dbContext;
    }
    
    
    public async Task<Gossip?> GetGossipByID (int id)
    {
        try
        {
            var gossip = await _gossipRepository.GetGossipByIdAsync(id);
            if (gossip == null) return null;

            return gossip;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while retrieving gossips with id {id}.", ex);
        }
    }

    public async Task<IEnumerable<Gossip>> GetAllGossips()
    {
        try
        {
            var gossips = await _gossipRepository.GetAllGossips();
            return gossips;
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception("An error occurred while retrieving gossips.", ex);
        }
    }


    public async Task<Gossip?> AddGossip(Gossip gossip)
    {
        try
        {
            var existingGossip = await _gossipRepository.GetGossipByIdAsync(gossip.Id);
            if (existingGossip != null)
            {
                throw new Exception($"Gossip with id {gossip.Id} already exists.");
            }
            var addedGossip = await _gossipRepository.AddGossipAsync(gossip);
            return addedGossip;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}, Details: {ex.InnerException?.Message}");
            throw new Exception("An error occurred while adding the gossip.", ex);
        }
        
    }

    
    public async Task DeleteGossip(int id)
    {
        try
        {
            var gossip = await _gossipRepository.GetGossipByIdAsync(id);
            if (gossip == null)
            {
                throw new Exception("community not found.");
            }

            await _gossipRepository.DeleteGossipAsync(id);
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception($"An error occurred while deleting gossip with id {id}.", ex);
        }
    }
}