using CryptocurrencyStatictics.Core.Common;
using CryptocurrencyStatictics.Core.Db.Repositories;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace CryptocurrencyStatictics.Core.Deals
{
    public class GetActualDealInfoQuery : IQuery<Result<GetActualDealInfoResult>>
    {
        [Required]
        public string Currencies { get; set; }
    }

    public class GetActualDealInfoResult
    {
        public string Currencies { get; set; }
        public decimal LastCost { get; set; }
        public DateTimeOffset UpdatedAtUtc { get; set; }
    }

    public class GetActualDealInfoHandler : IRequestHandler<GetActualDealInfoQuery, Result<GetActualDealInfoResult>>
    {
        private readonly DealRepository _dealRepository;

        public GetActualDealInfoHandler(DealRepository dealRepository)
            => _dealRepository = dealRepository;

        public Task<Result<GetActualDealInfoResult>> Handle(GetActualDealInfoQuery request, CancellationToken cancellationToken)
        {
            var lastDeal = _dealRepository.LastOrDefault(d => d.Currencies == request.Currencies);

            if (lastDeal == null)
                return Task.FromResult(Result<GetActualDealInfoResult>.Fail($"Deal info for currencies {request.Currencies} not found"));

            return Task.FromResult(Result<GetActualDealInfoResult>.Success(new GetActualDealInfoResult
            {
                Currencies = lastDeal.Currencies,
                LastCost = lastDeal.LastCost,
                UpdatedAtUtc = lastDeal.UpdatedAtUtc
            }));
        }
    }
}
