namespace c_sharp_backend.DTOs;

public class UserDTO
{
    
    public int id { get; set; }
    public string? username { get; set; }

    public string? lastname { get; set; }
    
    public string? password { get; set; }

    public string? email { get; set; }
}