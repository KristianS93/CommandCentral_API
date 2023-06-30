using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;

namespace Infrastructure.Services;

public class HouseholdService : IHouseholdService
{
    private readonly ApiDbContext _dbContext;
    private readonly ILogger<HouseholdService> _logger;
    
    //db context should probably be an interface
    public HouseholdService(ApiDbContext dbContext, ILogger<HouseholdService> logger)
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

}