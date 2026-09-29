using eShop.CoreBusiness.Models;
using eShop.CoreBusiness.Services;
using eShop.UseCases.PluginInterface.DataStore;
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
    public class PlaceOrderUseCase : IPlaceOrderUseCase
    {
        private readonly IOrderService orderService;
        private readonly IOrderRepository orderRepository;
        private readonly ISshoppingCartStateStore sshoppingCartStateStore;
        private readonly ISshoppingCart sshoppingCart;

        public PlaceOrderUseCase(
            IOrderService orderService,
            IOrderRepository orderRepository,
            ISshoppingCart sshoppingCart,
            ISshoppingCartStateStore sshoppingCartStateStore)
        {
            this.orderService = orderService;
            this.orderRepository = orderRepository;
            this.sshoppingCart = sshoppingCart;
            this.sshoppingCartStateStore = sshoppingCartStateStore;
        }

        public async Task<string> Execute(Order order)
        {
            if (orderService.ValidateCreateOrder(order))
            {
                order.DatePlaced = DateTime.Now;
                order.UniqueId = Guid.NewGuid().ToString();
                orderRepository.CreateOrder(order);

                await sshoppingCart.EmptyAsync();
                sshoppingCartStateStore.UpdateLineItemsCount();

                return order.UniqueId;
            }
            return null;
        }
    }
}
