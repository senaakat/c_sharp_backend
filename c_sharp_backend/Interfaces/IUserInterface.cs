using c_sharp_backend.DTOs;
using c_sharp.Models;

namespace c_sharp_backend.Interfaces;

public interface IUserInterface
{
    Task<IEnumerable<UserDTO>> GetAllUsers();
    
    Task<UserDTO?> GetUserOne(int id);
    Task<UserDTO?> AddUser(UserDTO userDto);
    
    Task<UserDTO?> UpdateUser(UserDTO userDto, int id);

    Task DeleteUser(int id);
}
