using CryptocurrencyStatictics.Core.Db;
using CryptocurrencyStatictics.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;

namespace CryptocurrencyStatictics.Core
{
    public static class DiBootstrapper
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    options => options.MigrationsAssembly("CryptocurrencyStatistics.Migrations")
                );
            });
        }
    }
}
