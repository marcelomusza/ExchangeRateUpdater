using ExchangeRateUpdater.Api.Filters;
using ExchangeRateUpdater.Application.Commands;
using ExchangeRateUpdater.Application.DTOs;
using ExchangeRateUpdater.Application.DTOs.Extensions;
using ExchangeRateUpdater.Application.Queries.CzechBank;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRateUpdater.Api.Controllers;

[ApiController]
[Route("api/exchange-rate")]
public class ExchangeRateController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ExchangeRateController> _logger;

    public ExchangeRateController(IMediator mediator, ILogger<ExchangeRateController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("czech-bank/process")]
    [ApiKey]
    public async Task<IActionResult> CzechBankProcessExchangeRates([FromBody] CzechBankRequestDto dto)
    {
        _logger.LogInformation("Retrieving exchange rates from Czech National Bank Api");

        var result = await _mediator.Send(dto.Map());

        if (result)
        {
            return Ok("Process completed successfully");
        }

        return BadRequest("Failed to retrieve and process exchange rates.");
    }

    [HttpGet("czech-bank/getbyday")]
    public async Task<IActionResult> CzechBankGetDailyExchangeRates([FromQuery] DateTime date)
    {
        _logger.LogInformation("Retrieving daily exchange rates for the Czech Bank");

        var exchangeRatesDto = await _mediator.Send(new GetExchangeRateByDayQuery(date));

        if (exchangeRatesDto == null)
        {
            return NotFound($"No exchange rates available for the given date: { date }");
        }

        return Ok(exchangeRatesDto);
    }
}
