using Azure;
using Azure.Data.Tables;

namespace SteamWebApp.Server.Data
{
    public class UserSummaryResponse
    {
        public User? userresponse { get; set; }
        public GameLibraryResponse? gamelibrary { get; set; }
    }
}