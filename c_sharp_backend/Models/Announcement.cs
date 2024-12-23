using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace c_sharp.Models;

public partial class Announcement {

    [Key]
    public int id { get; set; }
    
    public string ? Title { get; set; }
    
    public string ? Text { get; set; }
    
    public string ? Image { get; set; }
    
    public int ClickCount { get; set; }

}