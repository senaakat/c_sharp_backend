using c_sharp_backend.Config;
using c_sharp_backend.Interfaces;
using c_sharp_backend.Models;
using c_sharp_backend.Repository;
using c_sharp.Models;

namespace c_sharp_backend.Services;

public class ChatterCraftService:IChatterCraftInterface
{
    private readonly ChatterCraftRepository _chatterCraftRepository;
    private readonly UserRepository _userRepository;
    private readonly AppDbContext _dbContext;
    private readonly LikeRepository _likeRepository;

    public ChatterCraftService(AppDbContext dbContext, ChatterCraftRepository chatterCraftRepository, UserRepository userRepository,LikeRepository likeRepository)
    {
        _dbContext = dbContext;
        _chatterCraftRepository = chatterCraftRepository;
        _userRepository = userRepository;
        _likeRepository = likeRepository;

    }

    public async Task<ChatterCraft?> GetChatterCraft(int id)
    {
        try
        {
            var chatterCraft = await _chatterCraftRepository.GetChatAsync(id);
            if (chatterCraft == null) return null;

            return chatterCraft;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while retrieving chatterCraft with chat {id}.", ex);
        }
    }

   

    public async Task<IEnumerable<ChatterCraft>> GetAllChatterCrafts()
    {
        try
        {
            var chatterCrafts = await _chatterCraftRepository.GetAllChatsAsync();
            return chatterCrafts;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving chatterCraft.", ex);
        }
    }


    public async Task<ChatterCraft> AddChatterCraft(ChatterCraft chatterCraft)
    {
        try
        {
            var existingChatterCraft = await _chatterCraftRepository.GetChatAsync(chatterCraft.Id);
            if (existingChatterCraft != null)
            {
                throw new Exception($"This announcement already exists: {existingChatterCraft}");
            }

            var addedChatterCraft = await _chatterCraftRepository.AddChatAsync(chatterCraft);
            return addedChatterCraft;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}, Details: {ex.InnerException?.Message}");
            throw new Exception("An error occurred while adding the chat.", ex);
        }

    }


    public async Task<ChatterCraft> UpdateChatterCraft(ChatterCraft chatterCraft, int id)
    {
        try
        {
            var existingChatterCraft = await _chatterCraftRepository.GetChatAsync(id);
            if (existingChatterCraft == null)
            {
                throw new Exception("Chat not found.");
            }
            var likeCount = await _likeRepository.GetCountsforChatterCraftAsync(id);


            existingChatterCraft.Text = chatterCraft.Text;
            existingChatterCraft.Type = chatterCraft.Type;
            existingChatterCraft.Likes = likeCount;

            var updatedChatterCraft = await _chatterCraftRepository.UpdateChatAsync(existingChatterCraft);

            return updatedChatterCraft;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while updating chat with id {id}.", ex);
        }
    }

    public async Task DeleteChatterCraft(int id)
    {
        try
        {
            var chatterCraft = await _chatterCraftRepository.GetChatAsync(id);
            if (chatterCraft == null)
            {
                throw new Exception("chat not found.");
            }

            await _chatterCraftRepository.DeleteChatAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while deleting chat with id {id}.", ex);
        }
    }
}