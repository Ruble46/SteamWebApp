using Azure;
using Azure.Data.Tables;

namespace SteamWebApp.Server.Data
{
    public class GroupInvite : ITableEntity
    {
        #region TableEntity
        public required string PartitionKey { get; set; }
        public required string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        #endregion

        public string? Id { get; set; } //Invite code, 8 characters (uppercase/lowercase/numbers)
        public string? groupId { get; set; } //GUID
        public DateTime? creationDate { get; set; }
    }

    public class GroupInviteResponse
    {
        public List<GroupInvite>? groupInvites { get; set; }
    }
}