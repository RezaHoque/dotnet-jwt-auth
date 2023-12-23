using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet(Name = "HelloWorld")]        
        public string Get()
        {
            return "Hello World!";
        }
    }
}