using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
       IDataResult< List<Category>> GetAll();
        //GetbyId tek bir category çekiyoruz
        IDataResult< Category> GetById(int categoryId);

        IResult add(Category category );// void 
      

        IResult Update(Category category);


    }
}
