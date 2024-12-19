using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
            try
            {
                ClaimsIdentity? claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;

                string? nameIdentifier = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                string? steamId = nameIdentifier?.Split('/').Last();

                object? user = await _steamService.GetSteamUserSummaryAsync(steamId);
                if (user == null)
                {
                    return Unauthorized();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Steam user info: {ex.Message}");
                return Unauthorized();
            }
        }
    }
}
