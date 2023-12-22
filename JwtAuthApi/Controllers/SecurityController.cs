using Microsoft.AspNetCore.Mvc;
using JwtAuthApi.Models;
using JwtAuthApi.Helpers;

namespace JwtAuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AuthHelpers _authHelpers;

        public SecurityController(IConfiguration configuration)
        {
            _configuration = configuration;
            _authHelpers = new AuthHelpers(_configuration);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = _authHelpers.Authenticate(login);

            if (user != null && !string.IsNullOrEmpty(user.Username))
            {
                var tokenModel = _authHelpers.GenerateJSONWebToken(user);
                response = Ok(new {token = tokenModel.Token,expiration=tokenModel.Expiration});
            }

            return response;
        }
    }
}

        