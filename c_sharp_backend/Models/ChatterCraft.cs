using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using c_sharp_backend.Models;

namespace c_sharp.Models;

public partial class ChatterCraft {
    
    [Key]
    public int Id { get; set; }

    public string ? Text { get; set; }

    public int Likes { get; set; }

    public Type Type { get; set; }
    
    public int ? UserId { get; set; }
    public User ? User { get; set; }
}

public enum Type {
    Sınav,
    Kariyer,
    Edt,
    Soru,
    Etkinlik,
    Yemekhane
}