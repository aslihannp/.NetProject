using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    //Çıplak Class Kalmasın standardı
    public class Category:IEntity //IEntity category nin referensını tutabiliyor
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
