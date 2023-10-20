using Application.Contracts.Persistence;
using Domain.Entities.GroceryList;

namespace Application.Contracts.GroceryList;


/// <summary>
/// Documentation
/// </summary>
public interface IGroceryListItemRepository : IGenericRepository<GroceryListItemEntity>
{
    Task<List<GroceryListItemEntity>> GetGroceryListItemAsync(int groceryListId);

    Task<bool> IsOwnerOfItem(int groceryListId, int itemId);

}