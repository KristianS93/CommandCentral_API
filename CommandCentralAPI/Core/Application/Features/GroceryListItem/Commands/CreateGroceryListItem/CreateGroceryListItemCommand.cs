using System.Text.Json.Serialization;
using Application.Features.GroceryListItem.Shared;
using MediatR;

namespace Application.Features.GroceryListItem.Commands.CreateGroceryListItem;

public class CreateGroceryListItemCommand : IRequest<GroceryListItemDto>
{
    public string ItemName { get; set; } = string.Empty;
    public int ItemAmount { get; set; }
    
    [JsonIgnore]
    public int GroceryListId { get; set; }
}