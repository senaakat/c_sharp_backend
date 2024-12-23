using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using c_sharp_backend.Models;

namespace c_sharp.Models;

public partial class Teacher {

    [Key]
    public int Id { get; set; }

    public string ? TeacherGithub { get; set; }

    public ICollection < Lesson > ? Lessons { get; set; }

    public ICollection < LessonPdf > ? LessonPdfs { get; set; }
    
    public int ? UserId { get; set; }
    public User User { get; set; }

}