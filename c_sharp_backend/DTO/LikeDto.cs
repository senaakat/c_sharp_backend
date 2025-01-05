using c_sharp_backend.Models;

namespace c_sharp_backend.DTO;

public class LikeDto
{
    public int UserId { get; set; }
    public int ContentId { get; set; }
    public LikeType Type { get; set; }
}