using Microsoft.AspNetCore.Mvc;
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

    public async Task<object?> GetUserByIDAsync(string? steamId = null)
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
            JToken? player = data["response"]?["players"]?[0];
            return player as object;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Error fetching Steam user info: {ex.Message}");
        }
    }
}
