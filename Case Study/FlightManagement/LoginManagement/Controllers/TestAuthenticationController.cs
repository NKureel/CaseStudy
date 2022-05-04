using Common.Models;
using LoginManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace LoginManagement.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestAuthenticationController : ControllerBase
    {
        //private readonly IAdminAuthenticate _adminAuthenticate;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationUser> _roleManager;
        private readonly IConfiguration _configuration;
        public TestAuthenticationController(UserManager<ApplicationUser> user, RoleManager<ApplicationUser> role, IConfiguration configuration)
        {
            //_adminAuthenticate = admin;
            _userManager = user;
            _roleManager = role;
            _configuration = configuration;
        }
        [HttpGet]
        public string get()
        {
            return "Hello World";
        }


        //[AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]UserRegisterTbl userRegister)
        {
            Response response = null;
            try
            {
                var userExist = _userManager.FindByNameAsync(userRegister.UserName);
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
                    UserName=userRegister.UserName
                };
                var result =  _userManager.CreateAsync(user,userRegister.Password);
                if (!result.Result.Succeeded)
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
            return Ok(response);            
        }
    }
}
