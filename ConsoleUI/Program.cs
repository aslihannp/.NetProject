using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    //SOLID
    //O harfi: Open Closed Principle
    class Program
    {
        static void Main(string[] args)
        {
            //DTO: Data Transformation Object  -> join gibi şeyleri yapmamızı sağlar, örn product name in yanına categoryname koymayı sağlar
            //ProductTest();
            //IoC
            //CategoryTest();
            ProductTest();

        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            //ProductManager productManager = new ProductManager(new EfProductDal());//hangi veri yöntemi ile çalıştığımızı söylememiz lazım

            //var result = productManager.GetProductDetails();

            //if (result.Success == true)
            //{
            //    foreach (var product in result.Data)
            //    {
            //        Console.WriteLine(product.ProductName + " / " + product.CategoryName);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine(result.Message);
            //}

           
        }
    }
}
