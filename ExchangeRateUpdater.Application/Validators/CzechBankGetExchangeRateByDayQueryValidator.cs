using ExchangeRateUpdater.Application.Queries;
using ExchangeRateUpdater.Domain.Interfaces;
using FluentValidation;

namespace ExchangeRateUpdater.Application.Validators;

public class CzechBankGetExchangeRateByDayQueryValidator : AbstractValidator<CzechBankGetExchangeRateByDayQuery>
{
    private readonly IExchangeRateRepository _exchangeRateRepository;

    public CzechBankGetExchangeRateByDayQueryValidator(IExchangeRateRepository exchangeRateRepository)
    {
        _exchangeRateRepository = exchangeRateRepository;

        RuleFor(query => query.BankId)
    .MustAsync(BankExists).WithMessage("The specified Bank Id does not exist in the database.");
        _exchangeRateRepository = exchangeRateRepository;
    }

    private async Task<bool> BankExists(int bankId, CancellationToken cancellationToken)
    {
        var bank = await _exchangeRateRepository.GetBankAsync(bankId);
        return bank != null;
    }

}
