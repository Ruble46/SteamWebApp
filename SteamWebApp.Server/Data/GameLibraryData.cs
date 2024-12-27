public class Game
{
    public int appid { get; set; }
    public string? name { get; set; }
    public int playtime_forever { get; set; }
    public string? img_icon_url { get; set; }
    public bool has_community_visible_stats { get; set; }
}

public class GameLibraryResponse
{
    public int game_count { get; set; }
    public List<Game>? games { get; set; }
}
