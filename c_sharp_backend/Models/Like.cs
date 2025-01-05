using System.ComponentModel.DataAnnotations;

namespace c_sharp_backend.Models;

public class Like
{
    [Key]
    public int Id { get; set; }
    
    public LikeType Type { get; set; }
    public int ContentId { get; set; }
    
    public User  user { get; set; }
    
    public int UserId { get; set;}



    
}
public enum LikeType
{
    Announcement ,
    ChatterCraft
}