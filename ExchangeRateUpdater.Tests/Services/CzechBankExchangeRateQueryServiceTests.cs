using ExchangeRateUpdater.Application.Contracts.External;
using ExchangeRateUpdater.Application.DTOs;
using ExchangeRateUpdater.Application.Services;
using ExchangeRateUpdater.Domain.Interfaces;
using ExchangeRateUpdater.Domain.Model;
using Moq;

namespace ExchangeRateUpdater.Tests.Services;

[TestFixture]
public class CzechBankExchangeRateQueryServiceTests
{
    private Mock<ICzechBankExchangeRateService> _exchangeRateServiceMock;
    private Mock<IExchangeRateRepository> _exchangeRateRepositoryMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private CzechBankExchangeRateQueryService _service;

    [SetUp]
    public void Setup()
    {
        _exchangeRateServiceMock = new Mock<ICzechBankExchangeRateService>();
        _exchangeRateRepositoryMock = new Mock<IExchangeRateRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _unitOfWorkMock.SetupGet(u => u.ExchangeRateRepository).Returns(_exchangeRateRepositoryMock.Object);
        _service = new CzechBankExchangeRateQueryService(
            _exchangeRateServiceMock.Object,
            _exchangeRateRepositoryMock.Object,
            _unitOfWorkMock.Object
        );
    }

    [Test]
    public async Task GetExchangeRateByDayAsync_ShouldReturnMappedExchangeRatesForGivenDate()
    {
        // Arrange
        int bankId = 1;
        DateTime date = DateTime.UtcNow;
        var exchangeRates = new List<ExchangeRate>
            {
                new ExchangeRate(date, new Currency("CZK"), new Currency("USD"), 23.45M, new Bank("Czech National Bank")),
                new ExchangeRate(date, new Currency("CZK"), new Currency("EUR"), 25.67M, new Bank("Czech National Bank"))
            };

        _exchangeRateRepositoryMock.Setup(r => r.GetExchangeRatesByDayAsync(bankId, date)).ReturnsAsync(exchangeRates);

        // Act
        var result = await _service.GetExchangeRateByDayAsync(bankId, date);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.That(result.First().SourceCurrency, Is.EqualTo("CZK"));
        Assert.That(result.First().TargetCurrency, Is.EqualTo("USD"));
        _exchangeRateRepositoryMock.Verify(r => r.GetExchangeRatesByDayAsync(bankId, date), Times.Once);
    }

    [Test]
    public async Task GetExchangeRatesAsync_ShouldReturnFilteredAndFormattedExchangeRates()
    {
        // Arrange
        int bankId = 1;
        var currencyCodes = new List<CurrencyDto>
            {
                new CurrencyDto { Code = "USD" },
                new CurrencyDto { Code = "EUR" }
            };

        var allExchangeRates = new List<ExchangeRate>
            {
                new ExchangeRate(DateTime.UtcNow, new Currency("CZK"), new Currency("USD"), 23.45M, new Bank("Czech National Bank")),
                new ExchangeRate(DateTime.UtcNow, new Currency("CZK"), new Currency("EUR"), 25.67M, new Bank("Czech National Bank")),
                new ExchangeRate(DateTime.UtcNow, new Currency("CZK"), new Currency("GBP"), 30.12M, new Bank("Czech National Bank"))
            };

        _exchangeRateRepositoryMock.Setup(r => r.GetExchangeRatesByBankAsync(bankId)).ReturnsAsync(allExchangeRates);

        // Act
        var result = await _service.GetExchangeRatesAsync(bankId, currencyCodes);

        // Assert
        Assert.That(result.BankName, Is.EqualTo("Czech National Bank"));
        Assert.That(result.ExchangeRates.Count, Is.EqualTo(2));
        Assert.That(result.ExchangeRates.Any(rate => rate.Contains("CZK/USD")), Is.True);
        Assert.That(result.ExchangeRates.Any(rate => rate.Contains("CZK/EUR")), Is.True);
        Assert.That(result.ExchangeRates.Any(rate => rate.Contains("CZK/GBP")), Is.False);
        _exchangeRateRepositoryMock.Verify(r => r.GetExchangeRatesByBankAsync(bankId), Times.Once);
    }
}
