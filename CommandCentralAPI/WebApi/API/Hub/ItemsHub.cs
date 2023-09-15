using Application.Contracts.Hub;
using Application.Features.GroceryListItem.Shared;
using Microsoft.AspNetCore.SignalR;

namespace API.Hub;

public class ItemsHub : Hub<IItemsHub>
{
    async Task SendItemsToUser(List<GroceryListItemDto> items)
    {
        await Clients.All.SendItemsToUser(items);
    }
}