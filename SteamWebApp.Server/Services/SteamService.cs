using Newtonsoft.Json.Linq;

public class SteamService
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;

    public SteamService(string apiKey, HttpClient httpClient)
    {
        _apiKey = apiKey;
        _httpClient = httpClient;
    }

    public async Task<User?> GetUserByIDAsync(string? steamId = null)
    {
        if (string.IsNullOrEmpty(_apiKey) || string.IsNullOrEmpty(steamId))
        {
            throw new InvalidOperationException("Steam API key is not configured.");
        }

        string? url = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key={_apiKey}&steamids={steamId}";

        try
        {
            string? response = await _httpClient.GetStringAsync(url);
            JObject? data = JObject.Parse(response);

            // Check if user data exists
            UserResponse? usersData = data["response"]?.ToObject<UserResponse>();

            User? player = usersData?.players?.FirstOrDefault(new User());
            return player;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Error fetching Steam user info: {ex.Message}");
        }
    }

    public async Task<GameLibraryResponse> GetUserLibraryAsync(string? steamId = null)
    {
        if (string.IsNullOrEmpty(_apiKey) || string.IsNullOrEmpty(steamId))
        {
            throw new InvalidOperationException("Steam API key is not configured.");
        }

        string? url = $" http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={_apiKey}&steamid={steamId}&include_appinfo=1&format=json";

        try
        {
            string? response = await _httpClient.GetStringAsync(url);
            JObject? data = JObject.Parse(response);

            GameLibraryResponse? gameLibrary = data["response"]?.ToObject<GameLibraryResponse>();

            if (gameLibrary == null)
            {
                gameLibrary = new GameLibraryResponse();
            }

            return gameLibrary;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Error fetching Steam user info: {ex.Message}");
        }
    }
}
