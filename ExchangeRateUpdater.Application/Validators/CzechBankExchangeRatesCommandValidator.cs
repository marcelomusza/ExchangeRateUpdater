using ExchangeRateUpdater.Application.Commands;
using ExchangeRateUpdater.Application.DTOs.Enums;
using FluentValidation;

namespace ExchangeRateUpdater.Application.Validators
{
    public class CzechBankExchangeRatesCommandValidator : AbstractValidator<CzechBankExchangeRatesCommand>
    {
        public CzechBankExchangeRatesCommandValidator()
        {
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

        private bool BeADateInThePastOrToday(DateTime date)
        {
            return date <= DateTime.Today;
        }
    }
}
