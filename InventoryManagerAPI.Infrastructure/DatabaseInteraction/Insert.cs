using System.Data.SqlClient;
using InventoryManagerAPI.Domain.DatabaseInteraction;
using Z.Dapper.Plus;

namespace InventoryManagerAPI.Infrastructure.DatabaseInteraction;

public class Insert : IDatabaseBulkInsert
{
    public async Task InsertDataAsync<T>(string connectionString, string tableName, List<T> data) where T : class
    {
        DapperPlusManager.Entity<T>().Table(tableName);
        using var connection = new SqlConnection(connectionString);
        connection.BulkInsert(data);
    }
}