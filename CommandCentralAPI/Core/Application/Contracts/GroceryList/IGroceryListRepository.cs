using Domain.Entities.GroceryList;

namespace Application.Contracts.GroceryList;

public interface IGroceryListRepository
{
    Task<GroceryListEntity> GetGroceryListByHouseholdIdAsync(int householdId);
    
    Task DeleteAsync(GroceryListEntity groceryList);
    Task CreateAsync(GroceryListEntity groceryList);
}