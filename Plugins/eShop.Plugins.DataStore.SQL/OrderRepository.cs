using Dapper;
using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterface.DataStore;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace eShop.Plugins.DataStore.SQL
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Order GetOrder(int id)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var order = db.QueryFirstOrDefault<Order>("SELECT * FROM [Order] WHERE OrderId = @OrderId", new { OrderId = id });
            if (order != null)
            {
                order.LineItems = db.Query<OrderLineItem>("SELECT * FROM OrderLineItem WHERE OrderId = @OrderId", new { OrderId = id }).ToList();
            }
            return order;
        }

        public Order GetOrderByUniqueId(string uniqueId)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var order = db.QueryFirstOrDefault<Order>("SELECT * FROM [Order] WHERE UniqueId = @UniqueId", new { UniqueId = uniqueId });
            if (order != null)
            {
                order.LineItems = db.Query<OrderLineItem>("SELECT * FROM OrderLineItem WHERE OrderId = @OrderId", new { OrderId = order.OrderId }).ToList();
            }
            return order;
        }

        public int CreateOrder(Order order)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            
            var sqlOrder = @"
                INSERT INTO [Order] (DatePlaced, DateProcessing, DateProcessed, CustomerName, CustomerAddress, CustomerCity, CustomerStateProvince, CustomerCountry, AdminUser, UniqueId)
                VALUES (@DatePlaced, @DateProcessing, @DateProcessed, @CustomerName, @CustomerAddress, @CustomerCity, @CustomerStateProvince, @CustomerCountry, @AdminUser, @UniqueId);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";

            var orderId = db.QuerySingle<int>(sqlOrder, order);
            order.OrderId = orderId;

            if (order.LineItems != null && order.LineItems.Any())
            {
                var sqlLineItem = @"
                    INSERT INTO OrderLineItem (ProductId, OrderId, Quantity, Price)
                    VALUES (@ProductId, @OrderId, @Quantity, @Price);
                ";
                
                foreach(var item in order.LineItems)
                {
                    item.OrderId = orderId;
                    db.Execute(sqlLineItem, item);
                }
            }

            return orderId;
        }

        public void UpgradeOrder(Order order)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var sql = @"
                UPDATE [Order] 
                SET DatePlaced = @DatePlaced, 
                    DateProcessing = @DateProcessing, 
                    DateProcessed = @DateProcessed, 
                    CustomerName = @CustomerName, 
                    CustomerAddress = @CustomerAddress, 
                    CustomerCity = @CustomerCity, 
                    CustomerStateProvince = @CustomerStateProvince, 
                    CustomerCountry = @CustomerCountry, 
                    AdminUser = @AdminUser, 
                    UniqueId = @UniqueId
                WHERE OrderId = @OrderId
            ";
            db.Execute(sql, order);
        }

        public IEnumerable<Order> GetOrder()
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return db.Query<Order>("SELECT * FROM [Order]");
        }

        public IEnumerable<Order> GetOutStandingOrders()
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return db.Query<Order>("SELECT * FROM [Order] WHERE DateProcessed IS NULL");
        }

        public IEnumerable<Order> GetProcessedOrder()
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return db.Query<Order>("SELECT * FROM [Order] WHERE DateProcessed IS NOT NULL");
        }

        public IEnumerable<OrderLineItem> GetLineItemByOrderId(int orderId)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return db.Query<OrderLineItem>("SELECT * FROM OrderLineItem WHERE OrderId = @OrderId", new { OrderId = orderId });
        }
    }
}
