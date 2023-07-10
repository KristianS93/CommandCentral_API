namespace Domain.Models.ErrorResponses;

public class HouseHoldErrors : IErrorResponse
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int? Status { get; set; }
    public string Detail { get; set; }
    public string Instance { get; set; }


    public HouseHoldErrors HouseholdDoesNotExist(int id, string controllerPath)
    {
        Type = "/errors/household-e0001";
        Title = "Household does not exist";
        Status = 404;
        Detail = $"Retrieving the household id: {id} returned null, indicating the household doesnt exist";
        Instance = $"/{controllerPath}/{id}";
        return this;
    }

    public HouseHoldErrors HouseholdIdDoesNotMatchUpdatedHousehold(int id, string controllerPath, int itemId)
    {
        Type = "/errors/household-e0002";
        Title = "Household id mismatch with updated household";
        Status = 400;
        Detail = $"The id ({id}) provided for the household, and the id ({itemId}) provided in the updated household id does not match";
        Instance = $"/{controllerPath}/{id}";
        return this;
    }
}