namespace Domain.Models.ErrorResponses;

public class HouseHoldErrors : IErrorResponse
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int? Status { get; set; }
    public string Detail { get; set; }
    public string Instance { get; set; }


    public HouseHoldErrors HouseholdDoesNotExist(string controllerPath)
    {
        Type = "/errors/household-e0001";
        Title = "Household does not exist";
        Status = 404;
        Detail = $"Retrieving the household returned null, indicating the household doesnt exist";
        Instance = $"/{controllerPath}/";
        return this;
    }

    public HouseHoldErrors HouseholdIdDoesNotMatchUpdatedHousehold(string controllerPath, int itemId)
    {
        Type = "/errors/household-e0002";
        Title = "Household id mismatch with updated household";
        Status = 400;
        Detail = $"The provided household id, and the id ({itemId}) provided in the updated household id does not match";
        Instance = $"/{controllerPath}/";
        return this;
    }
}