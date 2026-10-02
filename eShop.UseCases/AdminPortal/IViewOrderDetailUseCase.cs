using eShop.CoreBusiness.Models;

namespace eShop.UseCases.AdminPortal
{
    public interface IViewOrderDetailUseCase
    {
        Order Execute(int orderId);
    }
}
