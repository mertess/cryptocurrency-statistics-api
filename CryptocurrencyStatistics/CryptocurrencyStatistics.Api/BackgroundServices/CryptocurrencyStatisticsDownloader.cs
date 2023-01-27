using CryptocurrencyStatictics.Core.Db;
using CryptocurrencyStatictics.Core.Db.DbModels;
using CryptocurrencyStatictics.Core.Db.Repositories;
using CryptocurrencyStatictics.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CryptocurrencyStatistics.Api.BackgroundServices
{
    public class CryptocurrencyStatisticsDownloader : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly TimeSpan _requestDelay;

        public CryptocurrencyStatisticsDownloader(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _requestDelay = TimeSpan.FromMinutes(1);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var dealRepository = scope.ServiceProvider.GetRequiredService<DealRepository>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();
            var yobitApiClient = scope.ServiceProvider.GetRequiredService<YobitApiClient>();

            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var currencies in Enum.GetValues(typeof(Currencies)).Cast<Currencies>())
                {
                    dealRepository.Create(await DownloadDealInfo(currencies, yobitApiClient));
                }

                await unitOfWork.SaveChangesAsync(stoppingToken);

                await Task.Delay(_requestDelay);
            }
        }

        private async Task<Deal> DownloadDealInfo(Currencies currencies, YobitApiClient yobitApiClient)
        {
            var response = await yobitApiClient.GetDealInfoByCurrencies(currencies);

            return new Deal
            {
                Currencies = YobitCurrenciesResolver.Resolve(currencies),
                LastCost = response.DealInfo.LastCost,
                UpdatedAtUtc = UnixTimeStampToDateTime(response.DealInfo.UpdatedAtUtc)
            };
        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
