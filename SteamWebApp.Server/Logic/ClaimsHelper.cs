using System.Security.Claims;

namespace SteamWebApp.Server.Logic
{
    public static class ClaimsHelper
    {
        public static string? GetID(ClaimsIdentity? claimsIdentity)
        {
            try
            {
                string? nameIdentifier = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return nameIdentifier?.Split('/').Last();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string? GetUserName(ClaimsIdentity claimsIdentity) 
        {
            try
            {
                return claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
