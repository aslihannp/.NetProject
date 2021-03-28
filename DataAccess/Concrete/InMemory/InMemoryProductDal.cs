using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            //yapay db oluşturduk
            //Oracle,sql server, postgress, MongoDb den geliyomuş gibi
            _products = new List<Product>() {
                new Product{ProductID=1, CategoryID=1, ProductName="Bardak", UnitPrice=15, UnitsInStock=15},
                new Product{ProductID=2, CategoryID=1, ProductName="Kamera", UnitPrice=500, UnitsInStock=3},
                new Product{ProductID=3, CategoryID=2, ProductName="Telefon", UnitPrice=1500, UnitsInStock=2},
                new Product{ProductID=4, CategoryID=2, ProductName="Klavye", UnitPrice=150, UnitsInStock=65},
                new Product{ProductID=5, CategoryID=2, ProductName="Mouse", UnitPrice=85, UnitsInStock=1}
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //_products.Remove(product); //YANLIŞ! REFERANS TİP sadece BU ŞEKİLDE SİLİNEMEZ
       
            /*LİNQ KULLANMADAN SİLME
            Product productToDelete=null;
            foreach (var p in _products)
            {
                if (product.ProductID == p.ProductID)
                {
                    productToDelete = p;
                }

                _products.Remove(productToDelete);
            }*/

            //LİNQ (Language Integrated )
            Product productToDelete = _products.SingleOrDefault(p=>p.ProductID==product.ProductID); //lambda fonk.u (foreach in yaptığı işi yaptı)
            //FirstOrDefault da kullşansak olur
            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll() //burada veri tabanındaki datayı business e eklemem lazım
        {
            return _products;// veritabanını olduğu gibi döndürüyoerum
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryID)
        {
            return _products.Where(p => p.CategoryID == categoryID).ToList();
            
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            //Gönderdiğim ürün id'sine sahip olan listedeki ürünü bul demek.
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductID == product.ProductID);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryID = product.CategoryID;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
            
        }
    }
}
