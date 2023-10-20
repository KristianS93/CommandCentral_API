using System.Text.Json.Serialization;
using MediatR;

namespace Application.Features.GroceryListItem.Commands.UpdateGroceryListItem;

public class UpdateGroceryListItemCommand : IRequest<Unit>
{
    public int GroceryListItemId{ get; set; }
    
    public string ItemName { get; set; } = string.Empty;
    
    public int ItemAmount { get; set; }

    [JsonIgnore]
    public int GroceryListId { get; set; }
}