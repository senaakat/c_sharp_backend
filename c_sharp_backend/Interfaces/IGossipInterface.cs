using c_sharp.Models;

namespace c_sharp_backend.Interfaces;

public interface IGossipInterface
{
    Task<IEnumerable<Gossip>> GetAllGossips();
    
    Task<Gossip?> GetGossipByID(int id);
    
    Task<Gossip?> AddGossip(Gossip gossip);
    
    Task DeleteGossip(int id);
}