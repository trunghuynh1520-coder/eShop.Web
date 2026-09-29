using eShop.CoreBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.UseCases.PluginInterface.DataStore
{
    public interface IProductRespository
    {
        // Khai báo các phương thức chờ được triển khai [7, 8]
        IEnumerable<Product> GetProducts(string filter = null);
        Product GetProduct(int id);
    }
}
