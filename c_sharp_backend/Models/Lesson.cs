using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace c_sharp.Models;

public partial class Lesson {

    [Key]
    public int Id { get; set; }

    public string ? LessonName { get; set; }

    public int ? TeacherId { get; set; }
    public Teacher ? Teacher { get; set; }

    public ICollection < LessonPdf? >?  LessonPdfs { get; set; }

}