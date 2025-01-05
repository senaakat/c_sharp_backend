using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace c_sharp.Models;

public partial class Community {
    [Key]
    public int Id { get; set; }

    public string  CommunityName { get; set; }

    public string ? Image { get; set; }
    [Required]
    [MaxLength(100)]
    public string  ShortName { get; set; }

    public string ? Text { get; set; }

}