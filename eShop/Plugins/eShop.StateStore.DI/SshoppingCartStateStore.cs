using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.UseCases.PluginInterface.StateStore;
using eShop.UseCases.PluginInterface.UI;

namespace eShop.StateStore.DI
{
    public class ShoppingCartStateStore : StateStoreBase, ISshoppingCartStateStore
    {
        private readonly ISshoppingCart shoppingCart;

        public ShoppingCartStateStore(ISshoppingCart shoppingCart)
        {
            this.shoppingCart = shoppingCart;
        }

        public async Task<int> GetItemsCountAsync()
        {
            var order = await shoppingCart.GetOrderAsync();
            if (order != null && order.LineItems != null)
            {
                return order.LineItems.Sum(x => x.Quantity);
            }
            return 0;
        }

        public void UpdateLineItemsCount()
        {
            BroadcastStateChange();
        }

        public void UpdateProductQuantity()
        {
            base.BroadcastStateChange();
        }
    }
}
