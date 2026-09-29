using System;
using System.Collections.Generic;
using System.Linq;

namespace eShop.CoreBusiness.Models
{
    public class Order
    {
        public int? OrderId { get; set; }
        public DateTime? DatePlaced { get; set; }
        public DateTime? DateProcessing { get; set; }
        public DateTime? DateProcessed { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerStateProvince { get; set; }
        public string CustomerCountry { get; set; }
        public string AdminUser { get; set; }
        public string UniqueId { get; set; }
        public List<OrderLineItem> LineItems { get; set; }

        public Order()
        {
            LineItems = new List<OrderLineItem>();
        }

        public void AddProduct(int productId, int quantity, double price)
        {
            var item = LineItems.FirstOrDefault(x => x.ProductId == productId);
            if (item == null)
            {
                LineItems.Add(new OrderLineItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Price = price
                });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void RemoveProduct(int productId)
        {
            var item = LineItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                LineItems.Remove(item);
            }
        }
    }
}
