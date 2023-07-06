using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IGroceryListItemService
{
    Task<List<GroceryListItemEntity>> GetAllAsync(int groceryListId);
    Task<GroceryListItemEntity> GetByIdAsync(int groceryListItem);
    Task UpdateAsync(GroceryListItemEntity item);
    Task CreateAsync(GroceryListItemEntity item);
    Task DeleteAsync(int itemId);
}