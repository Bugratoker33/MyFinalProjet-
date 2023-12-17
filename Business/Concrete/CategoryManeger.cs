using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManeger : ICategoryService
    { 
        //constractır injetion bağımlığı azaltıyoruz
        ICategoryDal _categoryDal;

        public CategoryManeger(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IResult add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult< List<Category>> GetAll()
        {
            //iş kodları yazarız 
         return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(),Messages.CateoriListed);

        }

        public IDataResult< Category> GetById(int categoryId)
        {
            //select* from Categor where categorId=3;
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == categoryId));

        }

        public IResult Update(Category category)
        {
          _categoryDal.Update(category);
            return new SuccessResult(Messages.ProductsListed);
        }
    }
}
