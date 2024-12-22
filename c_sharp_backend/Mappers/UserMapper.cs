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
            username = user.username,
            lastname = user.lastname,
            password = user.password,
            email = user.email
            
        };
    }
   
    public User MapUserDtoToUser(UserDto userDto)
    {
        return new User
        {
            username = userDto.username,
            lastname = userDto.lastname,
            password = userDto.password,
            email = userDto.email
        };
    }
    
}