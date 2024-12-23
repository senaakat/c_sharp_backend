using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using c_sharp_backend.Models;

namespace c_sharp.Models;
public partial class Gossip {

    [Key]
    public int Id { get; set; }

    public string ? Text { get; set; }

    public string ? Image { get; set; }
    
    public int ? UserId { get; set; }
    public User ? User { get; set; }

}