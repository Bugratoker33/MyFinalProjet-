using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // attrıbute 
    //route bize nasıl istekte bulunabileceklerounu gösterir 
    public class ProductsController : ControllerBase
    {// bu class bir controldır ve kendini ona göre yapılandır: 
       
        
        IProductService _productService;// filled:
        //product controlere diyor ki sen ıproduct service bağımlısın
        // IoC=ınversion of control ihtiyacımız var :
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("getall")] //request gerçekleştiriyor
        public IActionResult GetAll()
        {// dependes chain  == bağımlılık zinciri 
         //ı product servis bir producmanegere ihtiyaç duyuor prduct manegerde ıprductdala ihtiyaç duyuyor(efproduct dal):
         //     IProductService productService = new ProductManeger(new EfProductDal()); bunun 17 satırda code refactoring yaptık:

           // Thread.Sleep(5000);
            var result = _productService.GetAll();
            if (result.Succeess==true)
            {
              
                return Ok(result);//200 datanın kendisini
            }
            return BadRequest(result.Message);//400 mesajın kendisini vermiş olduk 

        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
        var result=_productService.GetById(id);
            if (result.Succeess == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbycategory")]
        public IActionResult GetByCategory(int categoryId)
        {
            var result = _productService.GetAllByCategoryId(categoryId);
            if (result.Succeess == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult add(Product product)
        {
            var result = _productService.add(product);

            if (result.Succeess==true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
