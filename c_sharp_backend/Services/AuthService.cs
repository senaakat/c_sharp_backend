using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using c_sharp_backend.Config;
using c_sharp_backend.DTO;
using c_sharp_backend.Mappers;
using c_sharp_backend.Models;
using c_sharp_backend.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;

namespace c_sharp_backend.Services;

public class AuthService
{
    private readonly AppDbContext _appDbContext;
    private readonly IConfiguration _configuration;
    private readonly UserRepository _userRepository;
    private readonly UserMapper _userMapper;

    public AuthService(AppDbContext appDbContext, IConfiguration configuration, UserRepository userRepository, UserMapper userMapper)
    {
        _appDbContext = appDbContext;
        _configuration = configuration;
        _userRepository = userRepository;
        _userMapper = userMapper;
    }

    public async Task<string?> Login(string username, string password)
    {
        var user = await _appDbContext.users
            .FirstOrDefaultAsync(u => u.username == username);

        if (user == null || user.password != password)
        {
            return null; 
        }

        // JWT token oluşturma
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.username),
            new Claim(ClaimTypes.Surname, user.lastname),
            new Claim(ClaimTypes.Email, user.email),
            new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
            new Claim(ClaimTypes.Role, user.role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public async Task<bool> Register(UserDto userDto)
    {
        try
        {
            var existingUser = await _appDbContext.users
                .FirstOrDefaultAsync(u => u.username == userDto.username || u.email == userDto.email);
            if (existingUser != null)
            {
                throw new Exception("User already exists.");
            }
            
            userDto.password = BCrypt.Net.BCrypt.HashPassword(userDto.password);
            
            var user = _userMapper.MapUserDtoToUser(userDto);
            var createdUser = await _userRepository.AddUserAsync(user);
            _userMapper.MapUserToUserDto(createdUser);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }
}