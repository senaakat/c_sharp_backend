using System.Collections.Generic;
using System;

namespace c_sharp.Models;

public partial class LessonPdf {

    public int id { get; set; }

    public string ? pdfName { get; set; }

    public int ? lessonId { get; set; }
    public Lesson ? lesson { get; set; }

    public int ? teacherId { get; set; }
    public Teacher ? teacher { get; set; }
}

//teacher ilişkisi
// lesson ilişkisi