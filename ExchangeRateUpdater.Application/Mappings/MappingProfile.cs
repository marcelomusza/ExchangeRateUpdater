using AutoMapper;
using ExchangeRateUpdater.Application.DTOs;
using ExchangeRateUpdater.Domain.Entities;

namespace ExchangeRateUpdater.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CzechBankExchangeRate, CzechBankExchangeRateDto>();
    }
}
