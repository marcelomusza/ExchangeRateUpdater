using ExchangeRateUpdater.Application.Contracts.External;
using ExchangeRateUpdater.Application.DTOs;
using ExchangeRateUpdater.Application.DTOs.Enums;
using ExchangeRateUpdater.Application.Services;
using ExchangeRateUpdater.Domain.Interfaces;
using ExchangeRateUpdater.Domain.Model;
using Moq;

namespace ExchangeRateUpdater.Tests.Services;

[TestFixture]
public class CzechBankExchangeRateProcessorServiceTests
{
    private Mock<ICzechBankExchangeRateService> _exchangeRateServiceMock;
    private Mock<IExchangeRateRepository> _exchangeRateRepositoryMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private CzechBankExchangeRateProcessorService _service;

    [SetUp]
    public void Setup()
    {
        _exchangeRateServiceMock = new Mock<ICzechBankExchangeRateService>();
        _exchangeRateRepositoryMock = new Mock<IExchangeRateRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _unitOfWorkMock.SetupGet(u => u.ExchangeRateRepository).Returns(_exchangeRateRepositoryMock.Object);
        _service = new CzechBankExchangeRateProcessorService(
            _exchangeRateServiceMock.Object,
            _exchangeRateRepositoryMock.Object,
            _unitOfWorkMock.Object
        );
    }

    [Test]
    public async Task ProcessExchangeRatesAsync_ShouldNotProcessIfRatesExistForDate()
    {
        // Arrange
        int bankId = 1;
        DateTime date = DateTime.UtcNow;
        Language language = Language.EN;
        _exchangeRateRepositoryMock.Setup(r => r.HasRatesForDateAsync(bankId, date)).ReturnsAsync(true);

        // Act
        var result = await _service.ProcessExchangeRatesAsync(bankId, date, language);

        // Assert
        Assert.IsFalse(result);
        _exchangeRateServiceMock.Verify(s => s.GetExchangeRatesAsync(It.IsAny<DateTime>(), It.IsAny<Language>()), Times.Never);
        _exchangeRateRepositoryMock.Verify(r => r.AddExchangeRatesAsync(It.IsAny<IEnumerable<ExchangeRate>>()), Times.Never);
    }

    [Test]
    public async Task ProcessExchangeRatesAsync_ShouldProcessAndSaveExchangeRatesIfNotAlreadyProcessed()
    {
        // Arrange
        int bankId = 1;
        DateTime date = DateTime.UtcNow;
        Language language = Language.EN;
        var bank = new Bank("Czech National Bank") { Id = 1 };

        var exchangeRatesDto = new List<CzechBankExchangeRateDto>
            {                
                new CzechBankExchangeRateDto
                {
                    ValidFor = date,
                    Order = 2,
                    Country = "United States",
                    Currency = "Dollar",
                    Amount = 1,
                    CurrencyCode = "USD",
                    Rate = 25.67M
                }
            };

        var currencies = new List<Currency>
            {
                new Currency("CZK"){ Id = 1 },
                new Currency("USD"){ Id = 2 },
                new Currency("EUR"){ Id = 3 }
            };

        _exchangeRateRepositoryMock.Setup(r => r.HasRatesForDateAsync(bankId, date)).ReturnsAsync(false);
        _exchangeRateRepositoryMock.Setup(r => r.GetBankAsync(bankId)).ReturnsAsync(bank);
        _exchangeRateServiceMock.Setup(s => s.GetExchangeRatesAsync(date, language)).ReturnsAsync(exchangeRatesDto);
        _exchangeRateRepositoryMock.Setup(r => r.GetCurrenciesListAsync()).ReturnsAsync(currencies);
        _exchangeRateRepositoryMock.Setup(r => r.GetSourceCurrencyAsync("CZK")).ReturnsAsync(currencies[0]);
        _exchangeRateRepositoryMock.Setup(r => r.AddExchangeRatesAsync(It.IsAny<IEnumerable<ExchangeRate>>())).Verifiable();
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(true);

        // Act
        var result = await _service.ProcessExchangeRatesAsync(bankId, date, language);

        // Assert
        Assert.IsTrue(result);
        _exchangeRateServiceMock.Verify(s => s.GetExchangeRatesAsync(date, language), Times.Once);
        _exchangeRateRepositoryMock.Verify(r => r.AddExchangeRatesAsync(It.IsAny<IEnumerable<ExchangeRate>>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}