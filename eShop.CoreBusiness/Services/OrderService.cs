using eShop.CoreBusiness.Models;

namespace eShop.CoreBusiness.Services
{
    public class OrderService : IOrderService
    {
        public bool ValidateCustomerInformation(string name, string address, string city, string province, string country)
        {
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(province) ||
                string.IsNullOrWhiteSpace(country))
                return false;

            return true;
        }

        public bool ValidateCreateOrder(Order order)
        {
            if (order == null) return false;
            if (order.LineItems == null || order.LineItems.Count == 0) return false;

            foreach (var item in order.LineItems)
            {
                if (item.ProductId <= 0 || item.Price < 0 || item.Quantity <= 0)
                    return false;
            }

            return ValidateCustomerInformation(order.CustomerName, order.CustomerAddress, order.CustomerCity, order.CustomerStateProvince, order.CustomerCountry);
        }

        public bool ValidateUpdateOrder(Order order)
        {
            if (order == null || !order.OrderId.HasValue || order.OrderId.Value <= 0) return false;
            return ValidateCreateOrder(order);
        }

        public bool ValidateProcessOrder(Order order)
        {
            if (order == null || !order.DateProcessing.HasValue || string.IsNullOrWhiteSpace(order.AdminUser))
                return false;

            return true;
        }
    }
}
