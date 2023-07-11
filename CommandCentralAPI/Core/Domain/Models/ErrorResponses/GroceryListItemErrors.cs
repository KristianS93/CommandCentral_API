namespace Domain.Models.ErrorResponses;

public class GroceryListItemErrors : IErrorResponse
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int? Status { get; set; }
    public string Detail { get; set; }
    public string Instance { get; set; }

    public GroceryListItemErrors ItemDoesNotExist(int id, string controllerPath)
    {
        Type = "/errors/grocerylistitem-e0001";
        Title = "Item does not exist";
        Status = 404;
        Detail = $"The requested item {id} does not exist.";
        Instance = $"/{controllerPath}/{id}";
        return this;
    }

    public GroceryListItemErrors IdDoesNotMatch(int updateId, int providedId, string controllerPath)
    {
        Type = "/errors/grocerylistitem-e0002";
        Title = "Id's does not match";
        Status = 400;
        Detail = $"The id {updateId} and the id in the item {providedId} does not match.";
        Instance = $"/{controllerPath}/{updateId}";
        return this;
    }
}