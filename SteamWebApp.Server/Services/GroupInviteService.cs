using Azure;
using Azure.Data.Tables;
using SteamWebApp.Server.Data;

public class GroupInviteService
{
    private readonly TableClient _tableClient;

    public GroupInviteService(IConfiguration configuration)
    {
        string? connectionString = configuration.GetSection("ConnectionStrings")["AzureStorageConnectionString"];
        string tableName = "groupinvites";

        _tableClient = new TableClient(connectionString, tableName);
    }

    public async Task InsertGroupInviteAsync(GroupInvite groupInvite)
    {
        await _tableClient.AddEntityAsync(groupInvite);
    }

    public async Task<GroupInvite?> GetGroupInviteAsync(string partitionKey, string rowKey)
    {
        try
        {
            var response = await _tableClient.GetEntityAsync<GroupInvite>(partitionKey, rowKey);
            return response.Value;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<List<GroupInvite>> GetGroupInvitesAsync(string partitionKey)
    {
        List<GroupInvite> groupInvites = new List<GroupInvite>();
        await foreach (var entity in _tableClient.QueryAsync<GroupInvite>(e => e.PartitionKey == partitionKey))
        {
            groupInvites.Add(entity);
        }

        return groupInvites;
    }

    public async Task UpdateGroupInviteAsync(GroupInvite groupInvite)
    {
        await _tableClient.UpdateEntityAsync(groupInvite, ETag.All, TableUpdateMode.Replace);
    }

    public async Task DeleteGroupInviteAsync(string partitionKey, string rowKey)
    {
        await _tableClient.DeleteEntityAsync(partitionKey, rowKey);
    }
}
