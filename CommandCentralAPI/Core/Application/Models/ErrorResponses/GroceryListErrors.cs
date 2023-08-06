namespace Domain.Models.ErrorResponses;

public class GroceryListErrors : IErrorResponse
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int? Status { get; set; }
    public string Detail { get; set; }
    public string Instance { get; set; }
    
    public GroceryListErrors GroceryListDoesNotExist(string controllerPath)
    {
        Type = "/errors/grocerylist-e0001";
        Title = "Grocery List does not exist";
        Status = 404;
        Detail = $"Retrieving the grocery list based on the household returned null, indicating the household does not have a grocery list.";
        Instance = $"/{controllerPath}";
        return this;
    }
    
    public GroceryListErrors GroceryListDuplicateHousehold(string controllerPath)
    {
        Type = "/errors/grocerylist-e0002";
        Title = "Household already has a grocery list";
        Status = 409;
        Detail = $"The household, already has an associated grocery list, a household can only contain 1 grocery list.";
        Instance = $"/{controllerPath}/";
        return this;
    }
    
}