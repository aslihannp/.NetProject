using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Product:IEntity
        //public bu class a diğer katmanlar da ulaşabilsin demek
        // bir class ın defaultu internal dır . internal dmek entities bu class a erişebilir demek.
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public short UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }

    }
}
