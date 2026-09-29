using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterface.DataStore;
using eShop.UseCases.SearchProductScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.UseCases.ViewProductScreen
{
    public class ViewProductUseCase: IViewProductUseCase
    {
        private readonly IProductRepository productRepository;

        // Constructor sử dụng Dependency Injection [9]
        public ViewProductUseCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public Product Execute(int id)
        {
            return productRepository.GetProduct(id);
        }

    }
}
