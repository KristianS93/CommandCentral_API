using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;

namespace Infrastructure.Services;

public class HouseholdService : IHouseholdService
{
    private readonly IApiDbContext _dbContext;
    private readonly ILogger<HouseholdService> _logger;
    
    //db context should probably be an interface
    public HouseholdService(IApiDbContext dbContext, ILogger<HouseholdService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    /// <summary>
    /// Gets all households
    /// </summary>
    /// <exception cref="ArgumentNullException">Source is null</exception>
    /// <exception cref="OperationCanceledException">If the CancellationToken is canceled.</exception>
    /// <returns>List of household objects</returns>
    public async Task<List<HouseholdEntity>> GetAllAsync()
    {
        // maybe user id requesting should be provided
        _logger.LogInformation("Requesting to retrieve all households");
        return await _dbContext.Household.ToListAsync();
    }

    public async Task<HouseholdEntity> GetByIdAsync(int id)
    {
        return await _dbContext.Household.FindAsync(id);
    }

    public async Task<int> CreateAsync(HouseholdEntity item)
    {
        _dbContext.Household.Add(item);
        await _dbContext.SaveChangesAsync();
        return item.Id;
    }

    public async Task UpdateAsync(HouseholdEntity item)
    {
        _dbContext.Household.Update(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(HouseholdEntity item)
    {
        _dbContext.Household.Remove(item);
        await _dbContext.SaveChangesAsync();
    }
}