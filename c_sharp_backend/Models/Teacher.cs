using System.Collections.Generic;
using System;

namespace c_sharp.Models;

//partial anahtar kelimesini kullanmasının amacı, User sınıfını birden fazla dosyada tanımlamak istemesidir.
// veri tipinden sonra ? kullanmasının nedeni, bu veri tipinin null olabileceğini belirtmektir.
public partial class Teacher {

    public int id { get; set; }

    public string ? teacherName { get; set; }

    public string ? teacherLastname { get; set; }

    public string ? teacherEmail {get; set; }

    public string ? teacherGithub { get; set; }

    public string ? teacherPassword { get; set; }

    public ICollection < Lesson > ? lessons { get; set; }

    public ICollection < LessonPdf > ? lessonPdfs { get; set; }

}

//lesson ilişkisi