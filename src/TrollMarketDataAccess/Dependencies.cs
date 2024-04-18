using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrollMarketDataAccess.Models;

namespace TrollMarketDataAccess;

public class Dependencies
{
    public static void ConfigureService(IConfiguration configuration, IServiceCollection services){
        services.AddDbContext<TrollMarketContext>(
            option=>option.UseSqlServer(configuration.GetConnectionString("TrollMarketConnection"))
        );
    }
}
