using MediatR;

namespace Application.Features.GroceryListItem.Commands.DeleteGroceryListItem;

public class DeleteGroceryListItemCommand : IRequest<Unit>
{
    public int GroceryListItemId { get; set; }
}