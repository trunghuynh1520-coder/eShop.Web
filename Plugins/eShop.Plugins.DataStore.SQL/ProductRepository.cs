using Dapper;
using eShop.CoreBusiness.Models;
using eShop.UseCases.SearchProductScreen;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace eShop.Plugins.DataStore.SQL
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Product GetProduct(int id)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return db.QueryFirstOrDefault<Product>("SELECT * FROM Product WHERE Id = @Id", new { Id = id });
        }

        public IEnumerable<Product> GetProducts(string filter)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            if (string.IsNullOrWhiteSpace(filter))
            {
                return db.Query<Product>("SELECT * FROM Product");
            }
            return db.Query<Product>("SELECT * FROM Product WHERE Name LIKE '%' + @Filter + '%'", new { Filter = filter });
        }
    }
}
