using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterface.UI;
using eShop.UseCases.ShoppingCartScreen.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCartScreen
{
    public class VIewShoppingCartUseCase : IViewShoppingCartUseCase
    {
        private readonly ISshoppingCart shoppingCart;
        public VIewShoppingCartUseCase(ISshoppingCart sshoppingCart)
        {
            this.shoppingCart = sshoppingCart;
        }
        public Task<Order> Execute()
        {
            return shoppingCart.GetOrderAsync();
        }
    }
}
