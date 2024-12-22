using System.Collections.Generic;
using System;
using c_sharp_backend.Models;

namespace c_sharp.Models;

public partial class Teacher {

    public int id { get; set; }

    public string ? teacherGithub { get; set; }

    public ICollection < Lesson > ? lessons { get; set; }

    public ICollection < LessonPdf > ? lessonPdfs { get; set; }
    
    public int ? userId { get; set; }
    public User user { get; set; }

}