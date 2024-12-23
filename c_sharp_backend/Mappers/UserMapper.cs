using AutoMapper;
using c_sharp_backend.DTO;
using c_sharp_backend.Models;

namespace c_sharp_backend.Mappers;

public class UserMapper:Profile
{
    public UserDto MapUserToUserDto(User user)
    {
        return new UserDto
        { 
            username = user.Username,
            lastname = user.Lastname,
            password = user.Password,
            email = user.Email
            
        };
    }
   
    public User MapUserDtoToUser(UserDto userDto)
    {
        return new User
        {
            Username = userDto.username,
            Lastname = userDto.lastname,
            Password = userDto.password,
            Email = userDto.email
        };
    }
    
}