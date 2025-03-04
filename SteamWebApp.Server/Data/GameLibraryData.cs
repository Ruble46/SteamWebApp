using Azure;
using Azure.Data.Tables;

namespace SteamWebApp.Server.Data
{
    public class Game : ITableEntity
    {
        #region TableEntity
        public required string PartitionKey { get; set; }
        public required string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        #endregion

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
}