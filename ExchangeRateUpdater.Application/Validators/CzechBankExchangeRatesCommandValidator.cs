using ExchangeRateUpdater.Application.Commands;
using ExchangeRateUpdater.Application.DTOs.Enums;
using ExchangeRateUpdater.Domain.Interfaces;
using FluentValidation;

namespace ExchangeRateUpdater.Application.Validators
{
    public class CzechBankExchangeRatesCommandValidator : AbstractValidator<CzechBankExchangeRatesCommand>
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;

        public CzechBankExchangeRatesCommandValidator(IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateRepository = exchangeRateRepository;

            RuleFor(query => query.BankId)
            .MustAsync(BankExists).WithMessage("The specified Bank Id does not exist in the database.");

            RuleFor(x => x.Date)
                .NotEmpty()
                    .WithMessage("Date is required.")
                .Must(BeADateInThePastOrToday)
                    .WithMessage("Date cannot be in the future.");

            RuleFor(x => x.Language)
                .NotEmpty()
                    .WithMessage("Language is required.")
                .IsInEnum()
                    .WithMessage("Invalid language selected. Please select a supported language.");
        }

        private async Task<bool> BankExists(int bankId, CancellationToken cancellationToken)
        {
            var bank = await _exchangeRateRepository.GetBankAsync(bankId);
            return bank != null;
        }

        private bool BeADateInThePastOrToday(DateTime date)
        {
            return date <= DateTime.Today;
        }
    }
}
