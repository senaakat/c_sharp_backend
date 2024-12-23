using c_sharp_backend.DTO;
using c_sharp_backend.Interfaces;
using c_sharp_backend.Mappers;
using c_sharp_backend.Models;
using c_sharp_backend.Repository;
using c_sharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace c_sharp_backend.Services;

public class UserService : IUserInterface
{
    private readonly UserRepository _userRepository;
    private readonly TeacherRepository _teacherRepository;
    private readonly UserMapper _userMapper;

    public UserService(UserRepository userRepository,TeacherRepository teacherRepository, UserMapper userMapper)
    {
        _userRepository = userRepository;
        _teacherRepository = teacherRepository;
        _userMapper = userMapper;
    }

    public async Task<UserDto?> GetUserOne(string email)
    {
        try
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null) return null;

            return _userMapper.MapUserToUserDto(user);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while retrieving user with id {email}.", ex);
        }
    }

    public async Task<IEnumerable<UserDto>> GetAllUsers()
    {
        try
        {
            var users = await _userRepository.GetAllUsersAsync();
            return  users.Select(user => _userMapper.MapUserToUserDto(user));
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving users.", ex);
        }
    }

    public async Task<UserDto?> GetUserId(int id)
    {
        try
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return null;

            return _userMapper.MapUserToUserDto(user);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while retrieving user with id {id}.", ex);
        }
    }

    public async Task<UserDto?> AddUser(UserDto userDto)
    {
        try
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(userDto.email);
            if (existingUser != null)
            {
                throw new Exception($"User with Email {userDto.email} already exists.");
            }
            if (string.IsNullOrEmpty(userDto.password))
            {
                throw new Exception("Password cannot be null or empty.");
            }
            userDto.password = BCrypt.Net.BCrypt.HashPassword(userDto.password);

            var user = _userMapper.MapUserDtoToUser(userDto);
            var createdUser = await _userRepository.AddUserAsync(user);
            return _userMapper.MapUserToUserDto(createdUser);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}, Details: {ex.InnerException?.Message}");
            throw new Exception("An error occurred while adding the user.", ex);
        }
        
    }  
    [HttpPost("/addAsTeacher")]
    public async Task<UserDto?> AddUserAsTeacher(UserDto userDto)
    {
        try
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(userDto.email);
            if (existingUser != null)
            {
                throw new Exception($"User with Email {userDto.email} already exists.");
            }
            if (string.IsNullOrEmpty(userDto.password))
            {
                throw new Exception("Password cannot be null or empty.");
            }
            userDto.password = BCrypt.Net.BCrypt.HashPassword(userDto.password);
            
            var user = _userMapper.MapUserDtoToUser(userDto);

            user.Role = Role.Teacher;

            var createdUser = await _userRepository.AddUserAsync(user);
            
            var teacher = new Teacher
            {
                UserId = createdUser.id,
            };
            
            await _teacherRepository.AddAsync(teacher);
            
            return _userMapper.MapUserToUserDto(createdUser);
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

            existingUser.Username = userDto.username;
            existingUser.Lastname = userDto.lastname;
            existingUser.Email = userDto.email;
            existingUser.Password = userDto.password;
            
            var updatedUser = await _userRepository.UpdateUserAsync(existingUser);
            
            return _userMapper.MapUserToUserDto(updatedUser);
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
            throw new Exception($"An error occurred while deleting user with id {id}.", ex);
        }
    }
}