using System.Collections.Generic;
using System;
using c_sharp_backend.Models;

namespace c_sharp.Models;

//partial anahtar kelimesini kullanmasının amacı, User sınıfını birden fazla dosyada tanımlamak istemesidir.
// veri tipinden sonra ? kullanmasının nedeni, bu veri tipinin null olabileceğini belirtmektir.
public partial class Teacher:User {

    public int id { get; set; }

    public string ? teacherGithub { get; set; }

    public ICollection < Lesson > ? lessons { get; set; }

    public ICollection < LessonPdf > ? lessonPdfs { get; set; }
    
    public int ? userId { get; set; }
    public User ? user { get; set; }

}