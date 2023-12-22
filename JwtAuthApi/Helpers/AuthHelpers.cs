using System.IdentityModel.Tokens.Jwt;
using System.Text;
using JwtAuthApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthApi.Helpers
{
    public class AuthHelpers
    {
        private readonly IConfiguration _configuration;

        public AuthHelpers(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenModel GenerateJSONWebToken(LoginModel userinfo)
        {
            //TODO:Consider user roles and claims based on the userinfo.
            var key = _configuration["Jwt:Key"];
            var issuer= _configuration["Jwt:Issuer"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key ?? string.Empty));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer,_configuration["Jwt:Issuer"],null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new TokenModel{Token = new JwtSecurityTokenHandler().WriteToken(token),Expiration = token.ValidTo};
  
        }
        public LoginModel Authenticate(LoginModel login)
        {
            if(login.Username=="johmdoe" && login.Password=="strongpassword_123")
            {
                return new LoginModel{Username = login.Username,Password = login.Password};
            }
            else
            {
                return new LoginModel();
            }
        }
    }
}