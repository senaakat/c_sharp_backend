using AutoMapper;
using c_sharp_backend.DTO;
using c_sharp_backend.DTOs;
using c_sharp_backend.Interfaces;
using c_sharp_backend.Repository;
using c_sharp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace c_sharp_backend.Services;

public class UserService : IUserInterface
{
    private readonly UserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly AppDbContext _dbContext;

    public UserService(AppDbContext _dbContext,UserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _dbContext = _dbContext;
    }

    public async Task<UserDTO?> GetUserOne(int id)
    {
        try
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return null;

            return _mapper.Map<UserDTO>(user);
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception($"An error occurred while retrieving user with id {id}.", ex);
        }
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsers()
    {
        try
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception("An error occurred while retrieving users.", ex);
        }
    }


    public async Task<UserDTO?> AddUser(UserDTO userDto)
    {
        try
        {
            // Veritabanında email ile eşleşen kullanıcı var mı kontrol et
            var existingUser = await _userRepository.GetUserByEmailAsync(userDto.email);
            if (existingUser != null)
            {
                throw new Exception($"User with Email {userDto.email} already exists.");
            }
            var user = _mapper.Map<User>(userDto);
            var createdUser = await _userRepository.AddUserAsync(user);
            return _mapper.Map<UserDTO>(createdUser);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}, Details: {ex.InnerException?.Message}");
            throw new Exception("An error occurred while adding the user.", ex);
        }
        
    }

    public async Task<UserDTO?> UpdateUser(UserDTO userDto, int id)
    {
        try
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            _mapper.Map(userDto, existingUser);
            var updatedUser = await _userRepository.UpdateUserAsync(existingUser);
            return _mapper.Map<UserDTO>(updatedUser);
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