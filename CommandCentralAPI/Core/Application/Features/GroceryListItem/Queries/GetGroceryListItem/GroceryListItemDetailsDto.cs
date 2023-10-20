namespace Application.Features.GroceryListItem.Queries.GetGroceryListItem;

public class GroceryListItemDetailsDto
{
    public int Id { get; set; }

    public string ItemName { get; set; } = string.Empty;

    public int ItemAmount { get; set; }
    
    public virtual DateTime CreatedAt { get; set; }
    
    public virtual DateTime LastModified { get; set; }
}