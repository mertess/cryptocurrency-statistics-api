using CryptocurrencyStatictics.Core.Deals;
using CryptocurrencyStatictics.Core.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CryptocurrencyStatistics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DealController(IMediator mediator) => _mediator = mediator;

        [HttpGet("[action]")]
        public async Task<ActionResult<GetActualDealInfoResult>> GetActualDealInfo(string currencies)
        {
            var result = await _mediator.Send(new GetActualDealInfoQuery { Currencies = currencies });
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<GetDealInfoByDateResult>> GetDealInfoByDate(DateTimeOffset byDate, string currencies)
        {
            var result = await _mediator.Send(new GetDealInfoByDateQuery { Currencies = currencies, ByDate = byDate });
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
