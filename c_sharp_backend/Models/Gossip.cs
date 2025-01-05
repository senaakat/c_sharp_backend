
using System.ComponentModel.DataAnnotations;

namespace c_sharp.Models;

public partial class Gossip {
    [Key]
    public int Id { get; set; }
    
    public string  Text { get; set; }

    [Required]
    public string Title {get; set;}

    public string ? Image { get; set; }
}