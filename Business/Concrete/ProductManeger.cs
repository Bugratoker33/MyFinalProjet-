using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.CSS;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Autofac.Transaction;
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
using System.Threading.Tasks;
using Core.Aspects.Autofac.Performance;

namespace Business.Concrete
{
    public class ProductManeger : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
        //entityFramework'e bağımlı olmadım o da IProductDal üzerinden gidiyo

        //bana ıprdocut referansı ver
        //prdoctmanger newlwndiğinde cotor diyor ki bana bir tane ı product dal refaransını ver 
        //bana efprdoucdal refaransını ver 

        public ProductManeger(IProductDal productDal ,ICategoryService categoryDal)
        {
            _productDal = productDal;
            _categoryService = categoryDal;

        }
        //Claim Yrtki anahtarı
        //[secureOperation("admin")]
       // [SecuredOperation( "product.add,admin" )]//yetkilendirme 
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        //IProductService
        public IResult add(Product product)
        {
         IResult result=   BussinessRules.Run(CheckIfProductCountCategoryCorrect(product.CategoryId),CheckIfProductNameExists(product.ProductName), CheckIfCategoryLimitExceded());
            //alt tarafdan kuralı ekle burada çalıştır çünkü burada kod spagetliye döner onun için busines params atatıdk istediğimiz kadar kural ekliyelim onu Iresulta atıyor o da gelip burdan döndürüyoruz 


            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            
            

            return new SuccessResult(Messages.ProductAdded);


            //    if (CheckIfProductCountCategoryCorrect(product.CategoryId).Succeess)
            //    {
            //       if (CheckIfProductNameExists(product.ProductName).Succeess)//ıresult döndürdüğümüzden succesi ekledik
            //       {
            //            _productDal.Add(product);

            //            return new SuccessResult(Messages.ProductAdded);
            //       }

            //    }
            //if (product.ProductName.Length < 2)
            //{ bu kodları validationla codrefactöring yaptık::
            //    //magic string
            //    return new ErrorResult(Messages.ProductNameInvalid);
            //}

            // ValidationTool.Validate(new ProductValidator(), product);
        }

        [CacheAspect]
       // [PerformanceAspect(5)]//5 sny geçerse haber ver 
        public IDataResult<List<Product>> GetAll()
        {
            //iş kodları varsa
            //Yetkisi var mı ?9
            if (DateTime.Now.Hour ==3 )
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);




        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountCategoryError);
                //Console.WriteLine("Daha fazla ekliyemezsiniz ");
            }
            throw new NotImplementedException();
        }

        private IResult CheckIfProductCountCategoryCorrect(int categoryId)
        {
            //select count(*) from products where categoryId=1
            //var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            //if (result >= 10)
            //{
            //    return new ErrorResult(Messages.ProductCountCategoryError);
            //    //Console.WriteLine("Daha fazla ekliyemezsiniz ");
            //}
            return new SuccessResult();
        }
        private IResult CheckIfProductNameExists(string producName)
        {
            var result = _productDal.GetAll(p => p.ProductName == producName).Any();
            if(result ==true)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);            
            
            }
            return new SuccessResult();
        }
     //   [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            add(product);
            if (product.UnitPrice < 10)
            {
                throw new Exception("");
            }

            add(product);

            return null;
        }
    }
}
