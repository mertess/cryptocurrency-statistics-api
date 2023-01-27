using CryptocurrencyStatictics.Core.Common.Extensions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptocurrencyStatictics.Core.Services
{
    public class YobitApiClient
    {
        private readonly HttpClient _httpClient;

        public YobitApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IYobitApiTickerResponse> GetDealInfoByCurrencies(Currencies currencies)
        {
            var yobitCurrencies = YobitCurrenciesResolver.Resolve(currencies);
            var endpointRoute = $"api/3/ticker/{yobitCurrencies}";

            var currentDelay = TimeSpan.FromSeconds(3);
            var tries = 5;

            while (true)
            {
                try
                {
                    return currencies switch
                    {
                        Currencies.EthBtc => await MakeGetRequest<YobitEthereumBitcoinResponse>(endpointRoute),
                        Currencies.EthUsd => await MakeGetRequest<YobitEthereumDollarResponse>(endpointRoute),
                        Currencies.BtcUsd => await MakeGetRequest<YobitBitcoinDollarResponse>(endpointRoute),
                        _ => throw new ArgumentException()
                    };
                }
                catch
                {
                    if (--tries == 0)
                        throw;

                    await Task.Delay(currentDelay);

                    currentDelay *= 2;
                }
            }
        }

        private async Task<TResult> MakeGetRequest<TResult>(string endpointRoute) 
            => (await _httpClient.GetStringAsync(endpointRoute)).FromJson<TResult>();
    }

    public static class YobitCurrenciesResolver
    {
        public static string Resolve(Currencies currencies)
        {
            return currencies switch
            {
                Currencies.EthBtc => "eth_btc",
                Currencies.EthUsd => "eth_usd",
                Currencies.BtcUsd => "btc_usd",
                _ => throw new ArgumentException()
            };
        }

        public static Currencies Resolve(string currencies)
        {
            return currencies switch
            {
                "eth_btc" => Currencies.EthBtc,
                "eth_usd" => Currencies.EthUsd,
                "btc_usd" => Currencies.BtcUsd,
                _ => throw new ArgumentException()
            };
        }
    }

    public interface IYobitApiTickerResponse
    {
        YobitDealInfo DealInfo { get; set; }
    }

    public class YobitEthereumDollarResponse : IYobitApiTickerResponse
    {
        [JsonProperty("eth_usd")]
        public YobitDealInfo DealInfo { get; set; }
    }

    public class YobitBitcoinDollarResponse : IYobitApiTickerResponse
    {
        [JsonProperty("btc_usd")]
        public YobitDealInfo DealInfo { get; set; }
    }

    public class YobitEthereumBitcoinResponse : IYobitApiTickerResponse
    {
        [JsonProperty("eth_btc")]
        public YobitDealInfo DealInfo { get; set; }
    }

    public class YobitDealInfo
    {
        [JsonProperty("last")]
        public decimal LastCost { get; set; }
        [JsonProperty("updated")]
        public long UpdatedAtUtc { get; set; }
    }

    public enum Currencies
    {
        EthUsd,
        EthBtc,
        BtcUsd
    }
}
