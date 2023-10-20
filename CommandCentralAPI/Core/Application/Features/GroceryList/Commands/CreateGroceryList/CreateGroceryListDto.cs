namespace Application.Features.GroceryList.Commands.CreateGroceryList;

public record CreateGroceryListDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    
}