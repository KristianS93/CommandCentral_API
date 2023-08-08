using Application.Features.GroceryListItem.Shared;
using MediatR;

namespace Application.Features.GroceryListItem.Commands.CreateGroceryListItem;

public class CreateGroceryListItemCommand : IRequest<GetGroceryListItemDto>
{
    public string ItemName { get; set; } = string.Empty;
    public int ItemAmount { get; set; }
    public int GroceryListId { get; set; }
}