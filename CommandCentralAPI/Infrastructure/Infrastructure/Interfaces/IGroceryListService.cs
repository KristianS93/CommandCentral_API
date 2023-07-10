using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IGroceryListService
{
    Task<GroceryListEntity> GetAsyncByHouseholdId(int householdId);
    Task DeleteAsync(int householdId);
    Task CreateAsync(int householdId);
}