using Domain.Entities;

namespace Infrastructure.Interfaces;

/// <summary>
/// This should be used if there is extra functionality needed for the household.
/// </summary>
public interface IHouseholdService
{
    public Task<List<HouseholdEntity>> GetAllAsync();
    public Task<HouseholdEntity> GetByIdAsync(int id);
    public Task<HouseholdEntity> CreateAsync(string householdName);
    public Task UpdateAsync(HouseholdEntity item);
    public Task DeleteAsync(int householdId);
}