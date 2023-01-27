using CryptocurrencyStatictics.Core.Common.Extensions;
using Microsoft.Extensions.Configuration;
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

        public async Task<IYobitApiTickerResponse> GetDealInfoByCurrency(Currency currency)
        {
            var endpointRoute = "api/3/ticker/{0}";

            var currentDelay = TimeSpan.FromSeconds(1);
            var tries = 5;

            while(true)
            {
                try
                {
                    return currency switch
                    {
                        Currency.EthBtc => (await _httpClient.GetStringAsync(string.Format(endpointRoute, "eth_btc"))).FromJson<YobitEthereumBitcoinResponse>(),
                        Currency.EthUsd => (await _httpClient.GetStringAsync(string.Format(endpointRoute, "eth_usd"))).FromJson<YobitEthereumDollarResponse>(),
                        Currency.BtcUsd => (await _httpClient.GetStringAsync(string.Format(endpointRoute, "btc_usd"))).FromJson<YobitBitcoinDollarResponse>(),
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

    public enum Currency
    {
        EthUsd,
        EthBtc,
        BtcUsd
    }
}
