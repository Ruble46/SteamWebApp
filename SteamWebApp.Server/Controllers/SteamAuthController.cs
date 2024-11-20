using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            return Challenge(new AuthenticationProperties { RedirectUri = "https://localhost:4200/app" }, "Steam");
        }

        [HttpGet("TestAuth")]
        [EnableCors("AllowFrontendApp")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult TestAuth()
        {
            if(HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            return Ok();
        }
    }
}
