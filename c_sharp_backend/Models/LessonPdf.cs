using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace c_sharp.Models;

public partial class LessonPdf {

    [Key]
    public int Id { get; set; }

    public string ? PdfName { get; set; }

    public int ? LessonId { get; set; }
    public Lesson ? Lesson { get; set; }

    public int ? TeacherId { get; set; }
    public Teacher ? Teacher { get; set; }
}
