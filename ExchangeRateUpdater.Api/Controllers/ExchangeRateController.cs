using ExchangeRateUpdater.Api.Filters;
using ExchangeRateUpdater.Application.DTOs;
using ExchangeRateUpdater.Application.DTOs.Extensions;
using ExchangeRateUpdater.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ExchangeRateUpdater.Api.Controllers;

[ApiController]
[Route("api/exchange-rate/czech-bank")]
public class ExchangeRateController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ExchangeRateController> _logger;

    public ExchangeRateController(IMediator mediator, ILogger<ExchangeRateController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("process")]
    [ApiKey]
    public async Task<IActionResult> CzechBankProcessExchangeRates([FromBody] CzechBankRequestDto dto)
    {
        var result = await _mediator.Send(dto.Map());

        if (result)
        {
            return Ok("Process completed successfully");
        }

        return BadRequest("Failed to retrieve and process exchange rates.");
    }

    [HttpGet("{bankId:int}/daily")]
    public async Task<IActionResult> CzechBankGetDailyExchangeRates(int bankId, [FromQuery] DateTime date)
    {
        var exchangeRatesDto = await _mediator.Send(new CzechBankGetExchangeRateByDayQuery(bankId, date));

        if (exchangeRatesDto == null)
        {
            return NotFound($"No exchange rates available for the given date: { date }");
        }

        return Ok(exchangeRatesDto);
    }

    [HttpGet("{bankId:int}/exchange-rates")]
    [EnableRateLimiting("ConcurrencyPolicy")]
    public async Task<IActionResult> CzechBankExchangeRates(int bankId, [FromQuery] string currencyCodes)
    {
        if (string.IsNullOrWhiteSpace(currencyCodes))
        {
            return BadRequest("Currency codes are required.");
        }

        var currencyList = currencyCodes.Split(',').Select(code => new CurrencyDto { Code = code });
        var response = await _mediator.Send(new CzechBankGetExchangeRatesQuery(bankId, currencyList));

        if (response == null)
        {
            return NotFound("No exchange rates available for the given currencies.");
        }

        return Ok(response);
    }
}
