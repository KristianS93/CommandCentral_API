using Domain.Entities;

namespace Infrastructure.Interfaces;

/// <summary>
/// This should be used if there is extra functionality needed for the household.
/// </summary>
public interface IHouseholdService: IBaseCRUD<HouseholdEntity>
{
    // public Task<List<HouseholdEntity>> GetAllAsync();
}