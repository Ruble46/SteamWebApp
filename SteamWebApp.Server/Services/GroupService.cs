using Azure;
using Azure.Data.Tables;
using SteamWebApp.Server.Data;

public class GroupService
{
    private readonly TableClient _tableClient;

    public GroupService(IConfiguration configuration)
    {
        string? connectionString = configuration.GetSection("ConnectionStrings")["AzureStorageConnectionString"];
        string tableName = "groups";

        _tableClient = new TableClient(connectionString, tableName);
    }

    public async Task InsertGroupAsync(Group group)
    {
        await _tableClient.AddEntityAsync(group);
    }

    public async Task<Group?> GetGroupAsync(string partitionKey, string rowKey)
    {
        try
        {
            var response = await _tableClient.GetEntityAsync<Group>(partitionKey, rowKey);
            return response.Value;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<List<Group>> GetGroupsAsync(string partitionKey)
    {
        List<Group> groups = new List<Group>();
        await foreach (var entity in _tableClient.QueryAsync<Group>(e => e.PartitionKey == partitionKey))
        {
            groups.Add(entity);
        }

        return groups;
    }

    public async Task UpdateGroupAsync(Group group)
    {
        await _tableClient.UpdateEntityAsync(group, ETag.All, TableUpdateMode.Replace);
    }

    public async Task DeleteGroupAsync(string partitionKey, string rowKey)
    {
        await _tableClient.DeleteEntityAsync(partitionKey, rowKey);
    }
}
