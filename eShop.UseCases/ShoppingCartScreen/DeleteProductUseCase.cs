using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterface.StateStore;
using eShop.UseCases.PluginInterface.UI;
using eShop.UseCases.ShoppingCartScreen.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCartScreen
{

    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly ISshoppingCart shoppingCart;
        private readonly ISshoppingCartStateStore sshoppingCartStateStore;
        public DeleteProductUseCase(ISshoppingCart shoppingCart, ISshoppingCartStateStore sshoppingCartStateStore)
        {
            this.shoppingCart = shoppingCart;
            this.sshoppingCartStateStore = sshoppingCartStateStore;
        }
        public async Task<Order> Execute(int Id)
        {
            var order = await this.shoppingCart.DeleteProductAsync(Id);
            this.sshoppingCartStateStore.UpdateLineItemsCount();
            return order;
        }
    }
}
