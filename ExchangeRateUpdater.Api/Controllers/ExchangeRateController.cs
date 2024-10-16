using ExchangeRateUpdater.Api.Filters;
using ExchangeRateUpdater.Application.Commands;
using ExchangeRateUpdater.Application.DTOs;
using ExchangeRateUpdater.Application.DTOs.Extensions;
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
}
