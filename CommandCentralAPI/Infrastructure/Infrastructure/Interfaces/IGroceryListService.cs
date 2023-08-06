using Domain.Entities;
using Domain.Entities.GroceryList;
using Domain.Exceptions.GroceryList;

namespace Infrastructure.Interfaces;

public interface IGroceryListService
{
    Task<GroceryListEntity> GetAsyncByHouseholdIdAsync(int householdId);
    Task DeleteAsync(int householdId);
    Task CreateAsync(int householdId);
}