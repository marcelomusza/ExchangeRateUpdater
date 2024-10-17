using ExchangeRateUpdater.Application.Queries;
using ExchangeRateUpdater.Domain.Interfaces;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ExchangeRateUpdater.Application.Validators;

public class CzechBankGetExchangeRatesQueryValidator : AbstractValidator<CzechBankGetExchangeRatesQuery>
{
    private readonly IExchangeRateRepository _exchangeRateRepository;

    public CzechBankGetExchangeRatesQueryValidator(IExchangeRateRepository exchangeRateRepository)
    {
        _exchangeRateRepository = exchangeRateRepository;

        RuleFor(query => query.BankId)
            .MustAsync(BankExists).WithMessage("The specified Bank Id does not exist in the database.");

        RuleFor(query => query.CurrencyCodes)
            .NotNull()
            .Must(c => c.Any())
            .Must(codes => codes.All(code => Regex.IsMatch(code.Code.ToUpper(), "^[A-Z]{3}$")))
                .WithMessage("The provided list of currencies is invalid or empty.");
    }

    private async Task<bool> BankExists(int bankId, CancellationToken cancellationToken)
    {
        var bank = await _exchangeRateRepository.GetBankAsync(bankId);
        return bank != null;
    }
}