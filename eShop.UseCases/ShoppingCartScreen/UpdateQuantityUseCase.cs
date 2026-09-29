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

    public class UpdateQuantityUseCase : IUpdateQuantityUseCase
    {
        private readonly ISshoppingCart shoppingCart;
        private readonly ISshoppingCartStateStore sshoppingCartStateStore;
        public UpdateQuantityUseCase(ISshoppingCart shoppingCart, ISshoppingCartStateStore shoppingCartStateStore)
        {
        }
        public async Task<Order> Execute(int Id, int Quantity)
        {
            var order = await this.shoppingCart.UpdateQuantityAsync(Id, Quantity);
            sshoppingCartStateStore.UpdateLineItemsCount();
            return order;
        }
    }
}
