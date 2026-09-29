using eShop.CoreBusiness.Models;

namespace eShop.UseCases.ShoppingCartScreen.interfaces
{
    public interface IUpdateQuantityUseCase
    {
        Task<Order> Execute(int Id, int Quantity);
    }
}