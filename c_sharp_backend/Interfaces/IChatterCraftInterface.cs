using c_sharp.Models;

namespace c_sharp_backend.Interfaces;

public interface IChatterCraftInterface
{
    Task<IEnumerable<ChatterCraft>> GetAllChatterCrafts();
    Task<ChatterCraft> GetChatterCraft(int id);
    Task<ChatterCraft> AddChatterCraft(ChatterCraft chatterCraft);
    Task<ChatterCraft> UpdateChatterCraft(ChatterCraft chatterCraft,int id);
    Task DeleteChatterCraft(int id);
    
}