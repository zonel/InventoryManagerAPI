using System.Data;
using System.Data.SqlClient;
using Dapper;
using InventoryManager.Application.Dto;
using InventoryManagerAPI.Domain.Configuration;
using InventoryManagerAPI.Domain.Repositories;

namespace InventoryManagerAPI.Infrastructure.Repository;

public class DatabaseRepository : IDatabaseRepository
{
    private readonly string _connectionString;

    public DatabaseRepository(IConfigurationManager configurationManager)
    {
        _connectionString = configurationManager.GetConnectionString();
    }

    public async Task<IEnumerable<T>> GetProductDataAsync<T>(string sku)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            string query = @"
            SELECT
                P.Name AS ProductName,
                P.EAN,
                P.ProducerName AS ProducerName,
                P.Category AS Category,
                P.DefaultImage AS ImageURL,
                I.Qty AS StockQuantity,
                I.Unit AS LogisticUnit,
                PR.NettProductPrice AS NetPurchasePrice,
                I.ShippingCost AS DeliveryCost
            FROM Products P
            LEFT JOIN Inventory I ON P.ID = I.ProductId
            LEFT JOIN Prices PR ON P.SKU = PR.SKU
            WHERE P.SKU = @Sku"; // Using parameterized query

            var result = await db.QueryAsync<T>(query, new { Sku = sku }); // Passing SKU as a parameter
            return result;
        }
    }
    }