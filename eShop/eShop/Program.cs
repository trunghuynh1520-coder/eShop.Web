using eShop.Components;
using eShop.CoreBusiness.Services;
using eShop.DataStore.HardCoded;
using eShop.ShoppingCartLocalStorage;
using eShop.StateStore.DI;
using eShop.UseCases.OrderConfirmationScreen;
using eShop.UseCases.PluginInterface.DataStore;
using eShop.UseCases.PluginInterface.StateStore;
using eShop.UseCases.PluginInterface.UI;
using eShop.UseCases.SearchProductScreen;
using eShop.UseCases.ShoppingCartScreen;
using eShop.UseCases.ShoppingCartScreen.interfaces;
using eShop.UseCases.ViewProductScreen;

namespace eShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddSingleton<IProductRepository, ProductRepository>();
            builder.Services.AddSingleton<IOrderRepository, OrderRepository>();

            builder.Services.AddTransient<IViewProductUseCase, ViewProductUseCase>();
            builder.Services.AddTransient<ISearchProductUseCase, SearchProductUseCase>();
            builder.Services.AddTransient<IOrderService, OrderService>();

            builder.Services.AddTransient<IAddProductToCartUseCase, AddProductToCartUseCase>();
            builder.Services.AddScoped<ISshoppingCart, ShoppingCart>();
            builder.Services.AddTransient<IViewShoppingCartUseCase, VIewShoppingCartUseCase>();
            builder.Services.AddScoped<ISshoppingCartStateStore, ShoppingCartStateStore>();
            builder.Services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();
            builder.Services.AddTransient<IUpdateQuantityUseCase, UpdateQuantityUseCase>();
            builder.Services.AddTransient<IPlaceOrderUseCase, PlaceOrderUseCase>();
            builder.Services.AddTransient<IViewOrderConfirmationUseCase, ViewOrderConfirmationUseCase >();



            var app = builder.Build(); 

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddAdditionalAssemblies(typeof(eShop.Web.CustomerPortal.Controls.ViewProductComponent).Assembly);

            app.Run();
        }
    }
}
