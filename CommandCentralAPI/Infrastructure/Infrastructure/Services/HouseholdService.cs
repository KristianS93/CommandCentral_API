using Domain.Entities;
using Domain.Exceptions;
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
        _logger.LogInformation("Requesting to retrieve all households");
        return await _dbContext.Household.ToListAsync();
    }

    public async Task<HouseholdEntity> GetByIdAsync(int id)
    {
        var item = await _dbContext.Household.FindAsync(id);
        if (item == null)
        {
            throw new HouseholdDoesNotExistException("Household does not exist");
        }
        return item;
    }

    public async Task<HouseholdEntity> CreateAsync(string householdName)
    {
        var household = new HouseholdEntity { Name = householdName };
        _dbContext.Household.Add(household);
        await _dbContext.SaveChangesAsync();
        return household;
    }

    public async Task UpdateAsync(HouseholdEntity item)
    {
        _dbContext.Household.Update(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await GetByIdAsync(id);
        if (item == null)
        {
            throw new HouseholdDoesNotExistException("Household does not exist");
        }
        _dbContext.Household.Remove(item);
        await _dbContext.SaveChangesAsync();
    }
}