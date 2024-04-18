using TrollMarketAPI.Admin;
using TrollMarketAPI.Authorizations;
using TrollMarketAPI.Cart;
using TrollMarketAPI.History;
using TrollMarketAPI.Merchandise;
using TrollMarketAPI.Profile;
using TrollMarketAPI.Shipments;
using TrollMarketAPI.Shop;
using TrollMarketBusiness.Interfaces;
using TrollMarketBusiness.Repositories;

namespace TrollMarketAPI.Configurations;

public static class ConfigureBusinessService
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services){
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<AuthService>();
        services.AddScoped<AdminService>();
        services.AddScoped<ProfileService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<MerchandiseService>();
        services.AddScoped<ShopService>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<CartService>();
        services.AddScoped<HistoryService>();
        services.AddScoped<IShipmentRepository, ShipmentRepository>();
        services.AddScoped<ShipmentService>();
        return services;
    }
}
