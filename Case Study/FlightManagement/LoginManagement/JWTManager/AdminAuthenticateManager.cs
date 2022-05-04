using Common.Models;
using LoginManagement.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace LoginManagement.JWTManager
{
    public class AdminAuthenticateManager : IAdminAuthenticate
    {
        Dictionary<string, string> adminLoginCredential = new Dictionary<string, string>()
        { {"admin","password" } };

        private readonly IConfiguration configuration;
        public AdminAuthenticateManager(IConfiguration config)
        {
            configuration = config;

        }
        public Tokens Authenticate(Admin admin)
        {
            if (!adminLoginCredential.Any(x => x.Key == admin.Name && x.Value == admin.Password))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            var tokendesc = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[] { new Claim(ClaimTypes.Name, admin.Name) }),
                Expires = System.DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokendesc);
            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}
