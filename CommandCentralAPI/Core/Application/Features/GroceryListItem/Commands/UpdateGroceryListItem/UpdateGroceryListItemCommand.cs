using MediatR;

namespace Application.Features.GroceryListItem.Commands.UpdateGroceryListItem;

public class UpdateGroceryListItemCommand : IRequest<Unit>
{
    public int Id { get; set; }
    
    public string ItemName { get; set; } = string.Empty;
    
    public int ItemAmount { get; set; }
}