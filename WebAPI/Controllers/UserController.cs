using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[HttpGet("getall")]
        //public IActionResult Getall()
        //{
        //    var result = _userService.GetAll();
        //    if (result.Succeess == true)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);

        //}
        //[HttpGet("getByEmail")]
        //public IActionResult GetByEmail(string email) 
        //{
        //    var result = _userService.GetByMail(email);
        //    if (result.Succeess == true)
        //    {
        //        return Ok(result);
        //    }
        //   return BadRequest(result);

        //}

    }
}
