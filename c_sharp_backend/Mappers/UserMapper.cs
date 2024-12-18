using AutoMapper;
using c_sharp_backend.DTOs;
using c_sharp_backend.Models;

namespace c_sharp_backend.Mappers;

public class UserMapper:Profile
{
    public static UserDTO MapUserToUserDto(User user)
    {
        if (user == null) return null;
    
        return new UserDTO
        { 
            username = user.username,
            lastname = user.lastname,
            password = user.password,
            email = user.email
            
        };
    }
   
    public User MapUserDtoToUser(UserDTO userDto)
    {
        if (userDto == null) return null;
    
        return new User
        {
            username = userDto.username,
            lastname = userDto.lastname,
            password = userDto.password,
            email = userDto.email
        };
    }
    
}