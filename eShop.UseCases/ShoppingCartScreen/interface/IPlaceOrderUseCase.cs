using eShop.CoreBusiness.Models;

namespace eShop.UseCases.ShoppingCartScreen.interfaces
{
    public interface IPlaceOrderUseCase
    {
        Task<string> Execute(Order order);
    }
}