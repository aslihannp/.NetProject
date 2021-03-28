using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constant;
using Business.VallidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService) //Constructor injection??? İncele
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        //Claim
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))] //attribute lara tipler typeof ile atılır
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //business codes

            //Eğer mevcut kategori sayısı 15i geçtiyse sisteme yeni ürün eklenemez.

            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName), CheckIfProductCountOfCategoryCorrect(product.CategoryID),CheckIfCatgoryLimitExceded());
            if (result != null)
            {
                return result;
            }

            //bir kategoride en fazla 15 ürün olabilir
            if (CheckIfProductCountOfCategoryCorrect(product.CategoryID).Success)
            {
                //Aynı isimde ürün eklenemez
                if (CheckIfProductNameExists(product.ProductName).Success)
                {
                    _productDal.Add(product);

                    return new SuccessResult(Messages.ProductAdded);
                    //return new SuccessResult();   -> Böyle de yollayabiliriz
                }

            }
            return new ErrorResult();

        }

        [CacheAspect]//key, value
        public IDataResult<List<Product>> GetAll()
        {
            //İş kodları
            //NOT: Bir iş sınıfı başka sınıfları new lemez, global değişken tanımalak lazım gibimsi
            if (DateTime.Now.Hour == 03)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryID == id));
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductID == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            //bir kategoride en fazla 10 ürün olabilir
            var result = _productDal.GetAll(p => p.CategoryID == product.CategoryID).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            throw new NotImplementedException();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)//iş kuralı parçasığı olduğu için private yapıyorum
        {

            //bir kategoride en fazla 15 ürün olabilir
            //select count(*) from products where categoryId=1
            var result = _productDal.GetAll(p => p.CategoryID == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductNameExists(string productName)
        {
            //Aynı isimde ürün eklenemez
            var result = _productDal.GetAll(p => p.ProductName == productName).Any(); //Any->buna uyan kayıt var mı? anlamında
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCatgoryLimitExceded()
        {

            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }

            return new SuccessResult();

        }

        //[TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            Add(product);
            if (product.UnitPrice<10)
            {
                throw new Exception("");
            }
            Add(product);
            return null;
        }
}   }
