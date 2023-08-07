using Application.Contracts.Persistence;
using Domain.Entities.GroceryList;

namespace Application.Contracts.GroceryList;

public interface IGroceryListItem : IGenericRepository<GroceryListItemEntity>
{
    
}