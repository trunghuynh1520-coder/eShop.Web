using System.Threading.Tasks;
using eShop.CoreBusiness.Models;

namespace eShop.UseCases.PluginInterface.UI
{
    public interface ISshoppingCart
    {
        Task<Order> AddProductAsync(Product product);
        Task<Order> DeleteProductAsync(int Id);
        Task<Order> GetOrderAsync();
        Task<Order> UpdateQuantityAsync(int Id, int quantity);
        Task<Order> UpdateOrderAsync(Order order);
        Task<Order> PlaceOrderAsync();
        Task EmptyAsync();
    }
}
