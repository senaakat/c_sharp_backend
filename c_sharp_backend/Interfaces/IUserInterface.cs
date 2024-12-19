using c_sharp_backend.DTOs;

namespace c_sharp_backend.Interfaces;

public interface IUserInterface
{
    Task<IEnumerable<UserDto>> GetAllUsers();
    
    Task<UserDto?> GetUserOne(String email);
    
    Task<UserDto?> GetUserId(int id);
    Task<UserDto?> AddUser(UserDto userDto);
    
    Task<UserDto?> UpdateUser(UserDto userDto, int id);

    Task DeleteUser(int id);
}
