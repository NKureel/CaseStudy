using Common.Models;
using LoginManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminAuthenticate adminAuthenticate;
        public AdminController(IAdminAuthenticate admin)
        {
            adminAuthenticate= admin;
        }
        [HttpGet]
        public string get()
        {
            return "Hello World";
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login(Admin admin)
        {
            var token = adminAuthenticate.Authenticate(admin);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);   
        }
    }
}
