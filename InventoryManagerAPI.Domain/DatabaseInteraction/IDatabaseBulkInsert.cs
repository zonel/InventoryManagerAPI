namespace InventoryManagerAPI.Domain.DatabaseInteraction;

public interface IDatabaseBulkInsert
{
    public Task InsertDataAsync<T>(string connectionString, string tableName, List<T> data) where T : class;
}