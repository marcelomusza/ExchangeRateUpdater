using ExchangeRateUpdater.Domain.Model;
using ExchangeRateUpdater.Infrastructure.Data;
using ExchangeRateUpdater.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRateUpdater.Tests.Repositories;

[TestFixture]
public class ExchangeRateRepositoryTests
{
    private ApplicationDbContext _context;
    private ExchangeRateRepository _repository;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new ExchangeRateRepository(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task AddExchangeRatesAsync_ShouldReturnTrue_WhenExchangeRatesAreAdded()
    {
        // Arrange
        var exchangeRates = new List<ExchangeRate>
        {
            new ExchangeRate(DateTime.UtcNow, new Currency("CZK"){ Id = 1 }, new Currency("USD"){ Id = 2 }, 23.45M, new Bank("Czech National Bank"){ Id = 1 })
        };

        // Act
        var result = await _repository.AddExchangeRatesAsync(exchangeRates);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task GetExchangeRatesByDayAsync_ShouldReturnRatesForSpecificDateAndBank()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var bank = new Bank("Czech National Bank");
        _context.Banks.Add(bank);
        _context.ExchangeRates.Add(new ExchangeRate(date, new Currency("CZK"), new Currency("USD"), 23.45M, bank));
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetExchangeRatesByDayAsync(bank.Id, date);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().TargetCurrency.Code, Is.EqualTo("USD"));
    }

    [Test]
    public async Task GetAllExchangeRatesAsync_ShouldReturnAllExchangeRates()
    {
        // Arrange
        _context.ExchangeRates.Add(new ExchangeRate(DateTime.UtcNow, new Currency("CZK"), new Currency("USD"), 23.45M, new Bank("Czech National Bank")));
        _context.ExchangeRates.Add(new ExchangeRate(DateTime.UtcNow, new Currency("CZK"), new Currency("EUR"), 25.67M, new Bank("Czech National Bank")));
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllExchangeRatesAsync();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task GetBankAsync_ShouldReturnBank_WhenBankExists()
    {
        // Arrange
        var bank = new Bank("Czech National Bank");
        _context.Banks.Add(bank);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetBankAsync(bank.Id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Czech National Bank"));
    }

    [Test]
    public async Task GetCurrenciesListAsync_ShouldReturnAllCurrencies()
    {
        // Arrange
        _context.Currencies.Add(new Currency("CZK"));
        _context.Currencies.Add(new Currency("USD"));
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetCurrenciesListAsync();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.That(result.Select(c => c.Code), Is.EquivalentTo(new[] { "CZK", "USD" }));
    }

    [Test]
    public async Task GetSourceCurrencyAsync_ShouldReturnCurrency_WhenCurrencyExists()
    {
        // Arrange
        _context.Currencies.Add(new Currency("CZK"));
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetSourceCurrencyAsync("CZK");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Code, Is.EqualTo("CZK"));
    }

    [Test]
    public async Task HasRatesForDateAsync_ShouldReturnTrue_WhenRatesExistForDateAndBank()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var bank = new Bank("Czech National Bank");
        _context.Banks.Add(bank);
        _context.ExchangeRates.Add(new ExchangeRate(date, new Currency("CZK"), new Currency("USD"), 23.45M, bank));
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.HasRatesForDateAsync(bank.Id, date);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task GetExchangeRatesByBankAsync_ShouldReturnRatesForSpecificBank()
    {
        // Arrange
        var bank = new Bank("Czech National Bank");
        _context.Banks.Add(bank);
        _context.ExchangeRates.Add(new ExchangeRate(DateTime.UtcNow, new Currency("CZK"), new Currency("USD"), 23.45M, bank));
        _context.ExchangeRates.Add(new ExchangeRate(DateTime.UtcNow, new Currency("CZK"), new Currency("EUR"), 25.67M, bank));
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetExchangeRatesByBankAsync(bank.Id);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.That(result.All(r => r.Bank.Id == bank.Id), Is.True);
    }
}
