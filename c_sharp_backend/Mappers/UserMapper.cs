using AutoMapper;
using c_sharp_backend.DTOs;
using c_sharp.Models;

namespace c_sharp_backend.Mappers;

public class UserMapper:Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();
    }
    
}