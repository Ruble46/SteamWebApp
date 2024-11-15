using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SteamWebApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SteamAuthController : ControllerBase
    {
        //[HttpPost("login")]
        [HttpGet("login")]
        [AllowAnonymous]
        [EnableCors("AllowFrontendApp")]
        public IActionResult Login()
        {
            // This will redirect the user to Steam's OpenID provider for authentication
            var result = Challenge(new AuthenticationProperties { RedirectUri = "/api/SteamAuth/app" }, "Steam");
            
            return result;
        }

        [HttpGet("app")]
        [AllowAnonymous]
        [EnableCors("AllowFrontendApp")]
        public IActionResult app()
        {
            return File("~/wwwroot/", "text/html");
        }
    }
}
