using ExternalData.Domain.Entities;

namespace ExternalData.Domain.Interfaces;

public interface IExchangeRateRepository
{
    Task AddAsync(ExchangeRateRecord record);

    Task<ExchangeRateRecord?> GetLatestAsync();

    Task<List<ExchangeRateRecord>> GetAllAsync();
}