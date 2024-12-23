using c_sharp_backend.DTO;

namespace c_sharp_backend.Interfaces;

public interface IUserInterface
{
    Task<IEnumerable<UserDto>> GetAllUsers();
    
    Task<UserDto?> GetUserOne(string email);
    
    Task<UserDto?> GetUserId(int id);
    Task<UserDto?> AddUser(UserDto userDto);

    Task<UserDto?> AddUserAsTeacher(UserDto userDto);
    
    Task<UserDto?> UpdateUser(UserDto userDto, int id);

    Task DeleteUser(int id);
}
