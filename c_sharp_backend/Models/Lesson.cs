using System.Collections.Generic;
using System;

namespace c_sharp.Models;

public partial class Lesson {

    public int id { get; set; }

    public string ? lessonName { get; set; }

    public int ? teacherId { get; set; }
    public Teacher ? teacher { get; set; }

    public ICollection < LessonPdf > ? lessonPdf { get; set; }

}

//teacher ilişkisi
// pdf ilişkisi