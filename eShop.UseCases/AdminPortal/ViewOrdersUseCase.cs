using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterface.DataStore;
using System.Collections.Generic;

namespace eShop.UseCases.AdminPortal
{
    public class ViewOrdersUseCase : IViewOrdersUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public ViewOrdersUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IEnumerable<Order> Execute()
        {
            return _orderRepository.GetOrder();
        }

        public IEnumerable<Order> GetOutstandingOrders()
        {
            return _orderRepository.GetOutStandingOrders();
        }

        public IEnumerable<Order> GetProcessedOrders()
        {
            return _orderRepository.GetProcessedOrder();
        }
    }
}
