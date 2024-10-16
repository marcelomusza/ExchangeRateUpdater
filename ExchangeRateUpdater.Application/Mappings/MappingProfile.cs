using AutoMapper;
using ExchangeRateUpdater.Application.DTOs;
using ExchangeRateUpdater.Domain.Entities;
using ExchangeRateUpdater.Domain.Model;

namespace ExchangeRateUpdater.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CzechBankExchangeRate, CzechBankExchangeRateDto>();
        CreateMap<ExchangeRate, ExchangeRateDto>();
    }
}
