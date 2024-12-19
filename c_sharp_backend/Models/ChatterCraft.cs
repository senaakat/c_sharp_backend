using System;
using System.Collections.Generic;
using c_sharp_backend.Models;

namespace c_sharp.Models;

public partial class ChatterCraft {

    public int id { get; set; }

    public string ? text { get; set; }

    public int likes { get; set; }

    public Type type { get; set; }
    
    public int ? userId { get; set; }
    public User ? user { get; set; }
}

public enum Type {
    Sınav,
    Kariyer,
    Edt,
    Soru,
    Etkinlik,
    Yemekhane
}