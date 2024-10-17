using AutoMapper;
using ExchangeRateUpdater.Application.DTOs;
using ExchangeRateUpdater.Application.DTOs.Enums;
using ExchangeRateUpdater.Domain.Entities;
using ExchangeRateUpdater.Domain.Model;
using ExchangeRateUpdater.Infrastructure.Factories;
using ExchangeRateUpdater.Infrastructure.Services.External;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Json;

namespace ExchangeRateUpdater.Tests.Services;

[TestFixture]
public class CzechBankExchangeRateServiceTests
{
    private Mock<IBankApiHttpClientFactory> _httpClientFactoryMock;
    private Mock<IMapper> _mapperMock;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private CzechBankExchangeRateService _service;

    [SetUp]
    public void SetUp()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(_httpMessageHandlerMock.Object) { BaseAddress = new Uri("https://api.mockbank.cz/") };

        _httpClientFactoryMock = new Mock<IBankApiHttpClientFactory>();
        _httpClientFactoryMock.Setup(f => f.CreateClient("CzechBank")).Returns(httpClient);

        _mapperMock = new Mock<IMapper>();
        _service = new CzechBankExchangeRateService(_httpClientFactoryMock.Object, _mapperMock.Object);
    }

    [Test]
    public async Task GetExchangeRatesAsync_ShouldReturnMappedExchangeRates_WhenApiResponseIsSuccessful()
    {
        // Arrange
        DateTime date = DateTime.UtcNow;
        Language language = Language.EN;
        var apiResponse = new ExchangeRatesResponse
        {
            Rates = new List<CzechBankExchangeRate>
            {
                new CzechBankExchangeRate { ValidFor = date, Order = 1, Country = "USA", Currency = "Dollar", Amount = 1, CurrencyCode = "USD", Rate = 23 },
                new CzechBankExchangeRate { ValidFor = date, Order = 2, Country = "EMU", Currency = "Euro", Amount = 1, CurrencyCode = "EUR", Rate = 25 }
            }
        };

        var expectedMappedResult = new List<CzechBankExchangeRateDto>
        {
            new CzechBankExchangeRateDto { ValidFor = date, Country = "USA", Currency = "Dollar", Amount = 1, CurrencyCode = "USD", Rate = 23 },
            new CzechBankExchangeRateDto { ValidFor = date, Country = "EMU", Currency = "Euro", Amount = 1, CurrencyCode = "EUR", Rate = 25 }
        };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(apiResponse)
            });

        _mapperMock.Setup(m => m.Map<IEnumerable<CzechBankExchangeRateDto>>(It.IsAny<IEnumerable<CzechBankExchangeRate>>()))
            .Returns(expectedMappedResult);

        // Act
        var result = await _service.GetExchangeRatesAsync(date, language);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Exactly(2).Items);
        Assert.That(result, Is.EquivalentTo(expectedMappedResult));
    }

    [Test]
    public void GetExchangeRatesAsync_ShouldThrowInvalidOperationException_WhenApiResponseIsUnsuccessful()
    {
        // Arrange
        DateTime date = DateTime.UtcNow;
        Language language = Language.EN;

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            });

        // Act & Assert
        Assert.That(async () => await _service.GetExchangeRatesAsync(date, language),
                    Throws.TypeOf<InvalidOperationException>()
                          .With.Message.EqualTo("Failed to retrieve exchange rates from the Czech Bank public Api"));
    }
}
