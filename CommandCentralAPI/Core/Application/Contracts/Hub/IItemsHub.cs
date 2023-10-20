using Application.Features.GroceryListItem.Shared;

namespace Application.Contracts.Hub;

public interface IItemsHub
{
    Task SendItemsToUser(List<GroceryListItemDto> items);
}