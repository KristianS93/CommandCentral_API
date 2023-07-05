using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IGroceryListService
{
    Task<GroceryListEntity> GetAsyncByHousehold(int householdId);
    Task DeleteAsync(GroceryListEntity item);
}