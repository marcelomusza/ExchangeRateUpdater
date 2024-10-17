using ExchangeRateUpdater.Application.Contracts;
using ExchangeRateUpdater.Application.Contracts.Services;
using ExchangeRateUpdater.Application.DTOs.Enums;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRateUpdater.Infrastructure.Jobs;

public class ExchangeRateJobScheduler : IExchangeRateJobScheduler
{
    private readonly IServiceProvider _serviceProvider;

    public ExchangeRateJobScheduler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

    }

    public void ScheduleDailyExchangeRateUpdate()
    {
        RecurringJob.AddOrUpdate(
            "czech-bank-daily-exchange-rate-update",
            () => CzechBankExecuteExchangeRateUpdate(DateTime.UtcNow.Date, Language.EN),
            Cron.Daily);
    }

    public async Task CzechBankExecuteExchangeRateUpdate(DateTime date, Language language)
    {
        using var scope = _serviceProvider.CreateScope();
        var processorService = scope.ServiceProvider.GetRequiredService<ICzechBankExchangeRateProcessorService>();

        await processorService.ProcessExchangeRatesAsync(1, date, language);
    }
}
