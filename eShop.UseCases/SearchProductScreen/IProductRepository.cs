using eShop.CoreBusiness.Models;

namespace eShop.UseCases.SearchProductScreen
{
    public interface IProductRepository
    {
        Product GetProduct(int id);
        IEnumerable<Product> GetProducts(string filter);
    }
}