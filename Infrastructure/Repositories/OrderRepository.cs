using LegacyOrderService.Domain.Interfaces;
using LegacyOrderService.Domain.Models;
using Microsoft.Data.Sqlite;

namespace LegacyOrderService.Infrastructure.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(string? dbPath = null)
        {
            var dbFile = dbPath ?? Path.Combine(AppContext.BaseDirectory, "orders.db");
            _connectionString = $"Data Source={dbFile}";
        }

        public void Save(Order order)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Orders (CustomerName, ProductName, Quantity, Price) VALUES ($name, $product, $qty, $price)";
            command.Parameters.AddWithValue("$name", order.CustomerName);
            command.Parameters.AddWithValue("$product", order.ProductName);
            command.Parameters.AddWithValue("$qty", order.Quantity);
            command.Parameters.AddWithValue("$price", order.Price);

            command.ExecuteNonQuery();
        }
    }
}
