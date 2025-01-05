
using System.ComponentModel.DataAnnotations;

namespace c_sharp.Models;

public partial class Announcement {
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string  Title { get; set; }
    public string ? Text { get; set; }
    public string ? Image { get; set; }
    public int Likes { get; set; }

}