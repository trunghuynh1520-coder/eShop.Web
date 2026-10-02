using eShop.CoreBusiness.Models;
using System.Collections.Generic;

namespace eShop.UseCases.AdminPortal
{
    public interface IViewOrdersUseCase
    {
        IEnumerable<Order> Execute();
        IEnumerable<Order> GetOutstandingOrders();
        IEnumerable<Order> GetProcessedOrders();
    }
}
