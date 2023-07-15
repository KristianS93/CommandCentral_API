namespace Domain.Models.ErrorResponses;

public class GroceryListErrors : IErrorResponse
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int? Status { get; set; }
    public string Detail { get; set; }
    public string Instance { get; set; }
    
    public GroceryListErrors GroceryListDoesNotExist(int id, string controllerPath)
    {
        Type = "/errors/grocerylist-e0001";
        Title = "Grocery List does not exist";
        Status = 404;
        Detail = $"Retrieving the grocery list based on the household id: {id} returned null, indicating the household does not have a grocery list.";
        Instance = $"/{controllerPath}/{id}";
        return this;
    }
    
    public GroceryListErrors GroceryListDuplicateHousehold(int id, string controllerPath)
    {
        Type = "/errors/grocerylist-e0002";
        Title = "Household already has a grocery list";
        Status = 409;
        Detail = $"The household {id}, already has an associated grocery list, a household can only contain 1 grocery list.";
        Instance = $"/{controllerPath}/{id}";
        return this;
    }
    
}