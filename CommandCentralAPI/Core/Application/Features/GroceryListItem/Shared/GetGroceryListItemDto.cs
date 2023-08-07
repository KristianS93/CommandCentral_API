namespace Application.Features.GroceryListItem.Shared;

public class GetGroceryListItemDto
{
    public int Id { get; set; }

    public string ItemName { get; set; } = string.Empty;

    public int ItemAmount { get; set; }
    
    // maybe created at to sort by date.
    
}