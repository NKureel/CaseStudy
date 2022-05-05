using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoginManagement.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]

    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticationController(UserManager<ApplicationUser> user, RoleManager<IdentityRole> role, IConfiguration configuration)
        {            
            _userManager = user;
            _roleManager = role;
            _configuration = configuration;
        }
        [HttpGet]
        [Authorize]
        [Authorize(Roles =UserRoles.Admin)]
        public string get()
        {
            return "Hello World";
        }


        //[AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterTbl userRegister)
        {
            Response response = new Response();
            try
            {
                var userExist = await _userManager.FindByNameAsync(userRegister.UserName);
                if (userExist != null)
                {
                    response.Message = "User Already Exists";
                    response.Status = "Error";
                    response.StatusCode = StatusCodes.Status500InternalServerError.ToString();
                    throw new Exception();
                }
                ApplicationUser user = new ApplicationUser()
                {
                    Email = userRegister.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = userRegister.UserName
                };
                var result = await _userManager.CreateAsync(user, userRegister.Password);
                if (!result.Succeeded)
                {
                    response.Message = "User Creation is failed";
                    response.Status = "Error";
                    response.StatusCode = StatusCodes.Status401Unauthorized.ToString();
                    throw new Exception();
                }

            }
            catch
            {
                return NotFound(response);
            }
            response.Status = "Success";
            response.Message = "User Created Successfully";
            response.StatusCode = StatusCodes.Status200OK.ToString();
            return Ok(response);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginTbl userLogin)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userLogin.UserName);
                if (user != null && await _userManager.CheckPasswordAsync(user, userLogin.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var authClaim = new List<Claim>{
                    new Claim(ClaimTypes.Name,user.UserName),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                    foreach (var userRole in userRoles)
                    {
                        authClaim.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:Issuer"],
                        audience: _configuration["JWT:Audience"],
                        expires: DateTime.Now.AddHours(5),
                        claims: authClaim,
                        signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256)
                        );
                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
            }
            catch { }
            Response response = new Response();
            response.Message = "Unauthorized User";
            response.Status = "Error";
            response.StatusCode = StatusCodes.Status401Unauthorized.ToString();
            return Unauthorized(response);
        }

        [HttpPost]
        [Route("registeradmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserRegisterTbl userRegister)
        {
            Response response = new Response();
            try
            {
                var userExist = await _userManager.FindByNameAsync(userRegister.UserName);
                if (userExist != null)
                {
                    response.Message = "User Already Exists";
                    response.Status = "Error";
                    response.StatusCode = StatusCodes.Status500InternalServerError.ToString();
                    throw new Exception();
                }
                ApplicationUser user = new ApplicationUser()
                {
                    Email = userRegister.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = userRegister.UserName
                };
                var result = await _userManager.CreateAsync(user, userRegister.Password);
                if (!result.Succeeded)
                {
                    response.Message = "User Creation is failed";
                    response.Status = "Error";
                    response.StatusCode = StatusCodes.Status401Unauthorized.ToString();
                    throw new Exception();
                }
                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                }
            }
            catch
            {
                return NotFound(response);
            }
            response.Status = "Success";
            response.Message = "User Created Successfully";
            response.StatusCode = StatusCodes.Status200OK.ToString();
            return Ok(response);
        }
    }
}
