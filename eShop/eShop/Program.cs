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

            var connectionString = builder.Configuration.GetConnectionString("eShop");
            
            builder.Services.AddTransient<IProductRepository>(sp => new eShop.Plugins.DataStore.SQL.ProductRepository(connectionString));
            builder.Services.AddTransient<IOrderRepository>(sp => new eShop.Plugins.DataStore.SQL.OrderRepository(connectionString));

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
            
            // Admin Use Cases
            builder.Services.AddTransient<eShop.UseCases.AdminPortal.IViewOrdersUseCase, eShop.UseCases.AdminPortal.ViewOrdersUseCase>();
            builder.Services.AddTransient<eShop.UseCases.AdminPortal.IProcessOrderUseCase, eShop.UseCases.AdminPortal.ProcessOrderUseCase>();
            builder.Services.AddTransient<eShop.UseCases.AdminPortal.IViewOrderDetailUseCase, eShop.UseCases.AdminPortal.ViewOrderDetailUseCase>();

            builder.Services.AddAuthentication("eShop.CookieAuth")
                .AddCookie("eShop.CookieAuth", options =>
                {
                    options.Cookie.Name = "eShop.CookieAuth";
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.AccessDeniedPath = "/access-denied";
                });
            builder.Services.AddAuthorization();
            builder.Services.AddCascadingAuthenticationState();

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
            
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddAdditionalAssemblies(
                    typeof(eShop.Web.CustomerPortal.Controls.ViewProductComponent).Assembly,
                    typeof(eShop.Web.AdminPortal.Pages.ManageOrdersComponent).Assembly
                );

            app.MapPost("/login", async (HttpContext context, [Microsoft.AspNetCore.Mvc.FromForm] string username, [Microsoft.AspNetCore.Mvc.FromForm] string password) =>
            {
                // Simple hardcoded login for demonstration
                if (username == "admin" && password == "admin")
                {
                    var claims = new System.Collections.Generic.List<System.Security.Claims.Claim>
                    {
                        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, username),
                        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "Admin")
                    };

                    var identity = new System.Security.Claims.ClaimsIdentity(claims, "eShop.CookieAuth");
                    var principal = new System.Security.Claims.ClaimsPrincipal(identity);

                    await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignInAsync(context, "eShop.CookieAuth", principal);
                    return Microsoft.AspNetCore.Http.Results.Redirect("/admin/orders");
                }
                return Microsoft.AspNetCore.Http.Results.Redirect("/login?error=InvalidCredentials");
            });

            app.MapGet("/logout", async (HttpContext context) =>
            {
                await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignOutAsync(context, "eShop.CookieAuth");
                return Microsoft.AspNetCore.Http.Results.Redirect("/");
            });

            app.Run();
        }
    }
}
