using Azure;
using Azure.Data.Tables;
using SteamWebApp.Server.Data;

public class UserService
{
    private readonly TableClient _tableClient;

    public UserService(IConfiguration configuration)
    {
        string? connectionString = configuration.GetSection("ConnectionStrings")["AzureStorageConnectionString"];
        string tableName = "users";

        _tableClient = new TableClient(connectionString, tableName);
    }

    public async Task InsertUserAsync(User user)
    {
        await _tableClient.AddEntityAsync(user);
    }

    public async Task<User?> GetUserAsync(string partitionKey, string rowKey)
    {
        try
        {
            var response = await _tableClient.GetEntityAsync<User>(partitionKey, rowKey);
            return response.Value;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<List<User>> GetUsersAsync(string partitionKey)
    {
        List<User> users = new List<User>();
        await foreach (var entity in _tableClient.QueryAsync<User>(e => e.PartitionKey == partitionKey))
        {
            users.Add(entity);
        }

        return users;
    }

    public async Task UpdateUserAsync(User user)
    {
        await _tableClient.UpdateEntityAsync(user, ETag.All, TableUpdateMode.Replace);
    }

    public async Task DeleteUserAsync(string partitionKey, string rowKey)
    {
        await _tableClient.DeleteEntityAsync(partitionKey, rowKey);
    }
}
