using System.Threading.Tasks;

namespace eShop.UseCases.ViewProductScreen
{
    public interface IAddProductToCartUseCase
    {
        void Execute(int productId);
    }
}
