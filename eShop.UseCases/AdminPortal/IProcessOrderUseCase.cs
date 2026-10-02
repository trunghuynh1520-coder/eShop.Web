using eShop.CoreBusiness.Models;

namespace eShop.UseCases.AdminPortal
{
    public interface IProcessOrderUseCase
    {
        bool Execute(int orderId, string adminUser);
    }
}
