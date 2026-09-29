using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterface.UI;
using eShop.UseCases.SearchProductScreen;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace eShop.ShoppingCartLocalStorage
{
    public class ShoppingCart : ISshoppingCart
    {
        private const string cstrShoppingCart = "eshop.Shoppingcart";
        private readonly IJSRuntime jsRunTime;
        private readonly IProductRepository productRepository;

        public ShoppingCart(IJSRuntime jsRunTime, IProductRepository productRepository)
        {
            this.jsRunTime = jsRunTime;
            this.productRepository = productRepository;
        }

        public async Task<Order> AddProductAsync(Product product)
        {
            var order = await GetOrder();
            order.AddProduct(product.Id, 1, product.Price);
            await SetOrder(order);
            return order;
        }

        public async Task<Order> DeleteProductAsync(int productId)
        {
            var order = await GetOrder();
            order.RemoveProduct(productId);
            await SetOrder(order);
            return order;
        }

        public Task EmptyAsync()
        {
            return this.SetOrder(null);
        }

        public async Task<Order> GetOrderAsync()
        {
            return await GetOrder();
        }

        public Task<Order> PlaceOrderAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            await this.SetOrder(order);
            return order;
        }

        public async Task<Order> UpdateQuantityAsync(int productId, int quantity)
        {
            var order = await GetOrder();
            if (quantity < 0)
                return order;
            else if (quantity == 0)
                return await DeleteProductAsync(productId);

            var lineItem = order.LineItems.SingleOrDefault(x => x.ProductId == productId);
            if (lineItem != null)
                lineItem.Quantity = quantity;

            await SetOrder(order);
            return order;
        }

        // --- ĐÃ SỬA: Đổi Task thành Task<Order> cho đồng bộ ---

        Task<Order> ISshoppingCart.AddProductAsync(Product product)
        {
            return AddProductAsync(product);
        }

        Task<Order> ISshoppingCart.DeleteProductAsync(int productId)
        {
            return DeleteProductAsync(productId);
        }

        Task<Order> ISshoppingCart.PlaceOrderAsync()
        {
            return PlaceOrderAsync();
        }

        Task<Order> ISshoppingCart.UpdateOrderAsync(Order order)
        {
            return UpdateOrderAsync(order);
        }

        Task<Order> ISshoppingCart.UpdateQuantityAsync(int productId, int quantity)
        {
            return UpdateQuantityAsync(productId, quantity);
        }

        private async Task<Order> GetOrder()
        {
            Order order = null;
            var strOrder = await jsRunTime.InvokeAsync<string>("localStorage.getItem", cstrShoppingCart);
            if (!string.IsNullOrEmpty(strOrder) && strOrder.ToLower() != "null")
            {
                order = JsonConvert.DeserializeObject<Order>(strOrder);
            }
            else
            {
                order = new Order();
                SetOrder(order);
            }

            foreach (var item in order.LineItems)
            {
                item.Product = productRepository.GetProduct(item.ProductId);
            }

            return order;
        }

        private async Task SetOrder(Order order)
        {
            await jsRunTime.InvokeVoidAsync("localStorage.setItem", cstrShoppingCart, JsonConvert.SerializeObject(order));
        }
    }
}