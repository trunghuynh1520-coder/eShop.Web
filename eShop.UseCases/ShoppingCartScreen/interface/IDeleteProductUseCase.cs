using eShop.CoreBusiness.Models;

namespace eShop.UseCases.ShoppingCartScreen.interfaces
{
    public interface IDeleteProductUseCase
    {
        Task<Order> Execute(int Id);
    }
}