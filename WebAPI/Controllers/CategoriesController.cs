using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet("getall")] //request gerçekleştiriyor
        public IActionResult GetAll()
        {
            
            var result = _categoryService.GetAll();
            if (result.Succeess == true)
            {

                return Ok(result);
            }
            return BadRequest(result);

        }


        [HttpPost("add")]
        public IActionResult add(Category category)
        {
            var result = _categoryService.add(category);

            if (result.Succeess == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Category category) 
        {
        
            var result=_categoryService.Update(category);
            if (result.Succeess == true)
            {
                return Ok(result);
            }
            return BadRequest(result);  

              
        }
    }
}
