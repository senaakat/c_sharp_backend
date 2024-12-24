using System.ComponentModel.DataAnnotations;
using c_sharp.Models;

namespace c_sharp_backend.Models;

//partial anahtar kelimesini kullanmasının amacı, User sınıfını birden fazla dosyada tanımlamak istemesidir.
// veri tipinden sonra ? kullanmasının nedeni, bu veri tipinin null olabileceğini belirtmektir.
public partial class User {

    [Key]
    public int id { get; set; }

    [Required]
    [MaxLength(255)]
    public string? Username { get; set; }

    [Required]
    [MaxLength(255)]
    public string? Lastname { get; set; }

    [Required]
    public string? Email { get; set; }
    
    [Required]
    public string? Password { get; set; }

    public Role Role { get; set; } = Role.User;
    
    public ICollection < Teacher > ? Teachers { get; set; }
    
    public ICollection < Gossip > ? Gossips { get; set; }
    
    public ICollection < ChatterCraft > ? ChatterCraft { get; set; }

}

public enum Role {
    User,
    Teacher,
    Admin
}