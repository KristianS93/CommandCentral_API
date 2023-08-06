using Domain.Entities;
using Domain.Entities.GroceryList;

namespace Infrastructure.Interfaces;

public interface IGroceryListItemService
{
    Task<GroceryListItemEntity> GetByIdAsync(int groceryListItem);
    Task UpdateAsync(GroceryListItemEntity item);
    Task CreateAsync(GroceryListItemEntity item);
    Task DeleteAsync(int itemId);
}