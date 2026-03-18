using ExternalData.Domain.Entities;
using ExternalData.Domain.Interfaces;
using ExternalData.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExternalData.Infrastructure.Repositories;

public class ExchangeRateRepository : IExchangeRateRepository
{
    private readonly AppDbContext _context;

    public ExchangeRateRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ExchangeRateRecord record)
    {
        await _context.ExchangeRateRecords.AddAsync(record);
        await _context.SaveChangesAsync();
    }

    public async Task<ExchangeRateRecord?> GetLatestAsync()
    {
        return await _context.ExchangeRateRecords
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<List<ExchangeRateRecord>> GetAllAsync()
    {
        return await _context.ExchangeRateRecords
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }
}