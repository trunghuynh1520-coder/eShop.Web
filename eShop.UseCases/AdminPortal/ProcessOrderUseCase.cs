using eShop.CoreBusiness.Models;
using eShop.CoreBusiness.Services;
using eShop.UseCases.PluginInterface.DataStore;
using System;

namespace eShop.UseCases.AdminPortal
{
    public class ProcessOrderUseCase : IProcessOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderService _orderService;

        public ProcessOrderUseCase(IOrderRepository orderRepository, IOrderService orderService)
        {
            _orderRepository = orderRepository;
            _orderService = orderService;
        }

        public bool Execute(int orderId, string adminUser)
        {
            var order = _orderRepository.GetOrder(orderId);
            if (order != null)
            {
                order.AdminUser = adminUser;
                order.DateProcessed = DateTime.Now;

                if (_orderService.ValidateProcessOrder(order))
                {
                    _orderRepository.UpgradeOrder(order);
                    return true;
                }
            }
            return false;
        }
    }
}
