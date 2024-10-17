﻿using ExchangeRateUpdater.Application.DTOs;
using MediatR;

namespace ExchangeRateUpdater.Application.Queries.CzechBank;

public record GetExchangeRateByDayQuery(int BankId, DateTime Date) : IRequest<IEnumerable<ExchangeRateDto>>;
