using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterface.DataStore;
using eShop.UseCases.PluginInterface.StateStore;
using eShop.UseCases.PluginInterface.UI;
using eShop.UseCases.SearchProductScreen;
using System;
using System.Threading.Tasks;

namespace eShop.UseCases.ViewProductScreen
{
    public class AddProductToCartUseCase : IAddProductToCartUseCase
    {
        private readonly ISshoppingCart shoppingCart;
        private readonly ISshoppingCartStateStore shoppingCartStateStore;
        private readonly IProductRepository productRepository;

        public AddProductToCartUseCase(
            ISshoppingCart shoppingCart,
            ISshoppingCartStateStore shoppingCartStateStore,
            IProductRepository productRepository)
        {
            this.shoppingCart = shoppingCart;
            this.shoppingCartStateStore = shoppingCartStateStore;
            this.productRepository = productRepository;
        }

        public void Execute(int productId)
        {
            var product = productRepository.GetProduct(productId);
            if (product != null)
            {
                shoppingCart.AddProductAsync(product); // Hoặc hàm add synchronous của bạn
                shoppingCartStateStore.UpdateLineItemsCount(); // Cập nhật số lượng thời gian thực
            }
        }

        public async Task ExecuteAsync(Product product)
        {
            await shoppingCart.AddProductAsync(product);
            shoppingCartStateStore.UpdateLineItemsCount(); // Cập nhật số lượng thời gian thực
        }
    }
}