using eShop.CoreBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.UseCases.PluginInterface.DataStore
{
    public interface IOrderRepository
    {
        Order GetOrder(int id);
        Order GetOrderByUniqueId(string uniqueId);
        int CreateOrder(Order order);
        void UpgradeOrder(Order order);
        IEnumerable<Order> GetOrder();
        IEnumerable<Order> GetOutStandingOrders();
        IEnumerable<Order> GetProcessedOrder();
        IEnumerable<OrderLineItem> GetLineItemByOrderId(int orderId);

    }

}
