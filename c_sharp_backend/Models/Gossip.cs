using System.Collections.Generic;
using System;

namespace c_sharp.Models;
//partial anahtar kelimesini kullanmasının amacı, User sınıfını birden fazla dosyada tanımlamak istemesidir.
// veri tipinden sonra ? kullanmasının nedeni, bu veri tipinin null olabileceğini belirtmektir.
public partial class Gossip {

    public int id { get; set; }

    public string ? text { get; set; }

    public string ? image { get; set; }

}