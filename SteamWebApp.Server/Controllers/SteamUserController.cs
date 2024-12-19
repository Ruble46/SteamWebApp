using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SteamWebApp.Server.Logic;
using System.Security.Claims;

namespace SteamWebApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SteamUserController : ControllerBase
    {
        private readonly SteamService _steamService;

        public SteamUserController(SteamService steamService)
        {
            _steamService = steamService;
        }

        [HttpGet("SteamUserSummary")]
        [EnableCors("AllowFrontendApp")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SteamUserSummary()
        {
            if (HttpContext.User.Identity == null)
            {
                return Unauthorized();
            }

            try
            {
                string? steamId = ClaimsHelper.GetID(HttpContext.User.Identity as ClaimsIdentity);

                if(steamId == null)
                {
                    return Problem("Failed to obtain SteamID from user authentication");
                }

                object? user = await _steamService.GetSteamUserSummaryAsync(steamId);
                if (user == null)
                {
                    return Unauthorized();
                }

                return Ok(user.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Steam user info: {ex.Message}");
                return Unauthorized();
            }
        }
    }
}
