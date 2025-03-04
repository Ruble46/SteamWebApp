using Azure;
using Azure.Data.Tables;

namespace SteamWebApp.Server.Data
{
    public class Group : ITableEntity
    {
        #region TableEntity
        public required string PartitionKey { get; set; }
        public required string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        #endregion

        public string? ownerId { get; set; } //ID of user that created group
        public bool? isAdmin { get; set; }
        public string? name { get; set; }
        public string? shortName { get; set; }
        public string? imageUrl { get; set; }
        public string? backgroundColor { get; set; }
        public string? blacklistIds { get; set; }
    }

    public class GroupResponse
    {
        public List<Group>? groups { get; set; }
    }
}