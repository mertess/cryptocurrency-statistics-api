using CryptocurrencyStatictics.Core.Common;
using CryptocurrencyStatictics.Core.Db.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CryptocurrencyStatictics.Core.Deals
{
    public class GetDealInfoByDateQuery : IQuery<Result<GetDealInfoByDateResult>>
    {
        public string Currencies { get; set; }
        public DateTimeOffset ByDate { get; set; }
    }

    public class GetDealInfoByDateResult
    {
        public string Currencies { get; set; }
        public decimal LastCost { get; set; }
        public DateTimeOffset UpdatedAtUtc { get; set; }
    }

    public class GetDealInfoByDateHandler : IRequestHandler<GetDealInfoByDateQuery, Result<GetDealInfoByDateResult>>
    {
        private readonly DealRepository _dealRepository;

        public GetDealInfoByDateHandler(DealRepository dealRepository)
            => _dealRepository = dealRepository;

        public Task<Result<GetDealInfoByDateResult>> Handle(GetDealInfoByDateQuery request, CancellationToken cancellationToken)
        {
            var deal = _dealRepository.FirstOrDefault(d => d.Currencies == request.Currencies && d.UpdatedAtUtc >= request.ByDate);

            if (deal == null)
                return Task.FromResult(Result<GetDealInfoByDateResult>.Fail($"Deal info for currencies {request.Currencies} and date {request.ByDate} not found"));

            return Task.FromResult(Result<GetDealInfoByDateResult>.Success(new GetDealInfoByDateResult
            {
                Currencies = deal.Currencies,
                LastCost = deal.LastCost,
                UpdatedAtUtc = deal.UpdatedAtUtc
            }));
        }
    }
}
