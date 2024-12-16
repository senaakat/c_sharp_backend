using System.Collections.Generic;
using System;

namespace c_sharp.Models;

public partial class Announcement {

    public int id { get; set; }
    public string ? title { get; set; }
    public string ? text { get; set; }
    public string ? image { get; set; }
    public int clickCount { get; set; }

}