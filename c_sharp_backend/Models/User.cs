using c_sharp.Models;

namespace c_sharp_backend.Models;

//partial anahtar kelimesini kullanmasının amacı, User sınıfını birden fazla dosyada tanımlamak istemesidir.
// veri tipinden sonra ? kullanmasının nedeni, bu veri tipinin null olabileceğini belirtmektir.
public partial class User {

    
    public int id { get; set; }

    public string? username { get; set; }

    public string? lastname { get; set; }

    public string? email { get; set; }
    public string? password { get; set; }

    public Role role { get; set; } = Role.USER;
    
    public ICollection < Teacher > ? teachers { get; set; }
    
    public ICollection < Gossip > ? gossips { get; set; }
    
    public ICollection < ChatterCraft > ? chatterCraft { get; set; }

}

public enum Role {
    USER,
    TEACHER,
    ADMIN
}