using CryptocurrencyStatictics.Core.Db;
using CryptocurrencyStatictics.Core.Db.Repositories;
using CryptocurrencyStatictics.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;
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

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<DealRepository>();
            services.AddScoped<UnitOfWork>();
        }
    }
}
