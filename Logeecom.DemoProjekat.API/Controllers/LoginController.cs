using Logeecom.DemoProjekat.BL.Services;
using Logeecom.DemoProjekat.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Logeecom.DemoProjekat.PL.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly LoginService loginService;
        private readonly IConfiguration Configuration;

        public LoginController(LoginService loginService, IConfiguration configuration)
        {
            this.loginService = loginService;
            this.Configuration = configuration;
        }

        [HttpPost]
        public string Post(string username, string password)
        {
            return this.loginService.Login(
                new User { Username = username, Password = password },
                this.Configuration["Jwt:Key"],
                this.Configuration["Jwt:Issuer"],
                this.Configuration["Jwt:Audience"]);
        }
    }
}
