using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace c_sharp.Models;

public partial class Community {
    
    [Key]
    public int id { get; set; }

    public string ? CommunityName { get; set; }

    public string ? Image { get; set; }

    public string ? ShortName { get; set; }

    public string ? Text { get; set; }

}