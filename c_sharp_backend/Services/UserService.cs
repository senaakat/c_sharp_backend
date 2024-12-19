using c_sharp_backend.Config;
using c_sharp_backend.DTO;
using c_sharp_backend.Interfaces;
using c_sharp_backend.Mappers;
using c_sharp_backend.Repository;

namespace c_sharp_backend.Services;

public class UserService : IUserInterface
{
    private readonly UserRepository _userRepository;
    private readonly UserMapper _userMapper;
    private readonly AppDbContext _appDbContext;

    public UserService(AppDbContext appDbContext,UserRepository userRepository, UserMapper userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
        _appDbContext = appDbContext;
    }

    public async Task<UserDto?> GetUserOne(string email)
    {
        try
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null) return null;

            return UserMapper.MapUserToUserDto(user);
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception($"An error occurred while retrieving user with id {email}.", ex);
        }
    }

    public async Task<IEnumerable<UserDto>> GetAllUsers()
    {
        try
        {
            var users = await _userRepository.GetAllUsersAsync();
            return  users.Select(user => UserMapper.MapUserToUserDto(user));
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception("An error occurred while retrieving users.", ex);
        }
    }

    public async Task<UserDto?> GetUserId(int id)
    {
        try
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return null;

            return UserMapper.MapUserToUserDto(user);
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception($"An error occurred while retrieving user with id {id}.", ex);
        }
    }

    public async Task<UserDto?> AddUser(UserDto userDto)
    {
        try
        {
            // Veritabanında email ile eşleşen kullanıcı var mı kontrol et
            var existingUser = await _userRepository.GetUserByEmailAsync(userDto.email);
            if (existingUser != null)
            {
                throw new Exception($"User with Email {userDto.email} already exists.");
            }
            var user = _userMapper.MapUserDtoToUser(userDto);
            var createdUser = await _userRepository.AddUserAsync(user);
            return UserMapper.MapUserToUserDto(createdUser);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}, Details: {ex.InnerException?.Message}");
            throw new Exception("An error occurred while adding the user.", ex);
        }
        
    }

    public async Task<UserDto?> UpdateUser(UserDto userDto, int id)
    {
        try
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            existingUser.username = userDto.username;
            existingUser.lastname = userDto.lastname;
            existingUser.email = userDto.email;
            existingUser.password = userDto.password;
            
            var updatedUser = await _userRepository.UpdateUserAsync(existingUser);
            
            return UserMapper.MapUserToUserDto(updatedUser);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while updating user with id {id}.", ex);
        }
    }

    public async Task DeleteUser(int id)
    {
        try
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            await _userRepository.DeleteUserAsync(id);
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception($"An error occurred while deleting user with id {id}.", ex);
        }
    }
}