using ExchangeRateUpdater.Application.DTOs.Enums;

namespace ExchangeRateUpdater.Application.Contracts;

public interface IExchangeRateJobScheduler
{
    void ScheduleDailyExchangeRateUpdate();
}
