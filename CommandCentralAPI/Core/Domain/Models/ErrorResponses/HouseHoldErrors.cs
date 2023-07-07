namespace Domain.Models.ErrorResponses;

public class HouseHoldErrors : IErrorResponse
{
    public HouseHoldErrors(int id, string controllerPath)
    {
        _id = id;
        _controllerPath = controllerPath;
    }
    
    private int _id;
    private string _controllerPath;
    
    public string Type { get; set; }
    public string Title { get; set; }
    public int? Status { get; set; }
    public string Detail { get; set; }
    public string Instance { get; set; }


    public HouseHoldErrors HouseholdDoesNotExist()
    {
        Type = "/errors/household-e0001";
        Title = "Household does not exist";
        Status = 404;
        Detail = $"Retrieving the household id: {_id} returned null, indicating the household doesnt exist";
        Instance = $"/{_controllerPath}";
        return this;
    }

    public HouseHoldErrors HouseholdIdDoesNotMatchUpdatedHousehold(int itemId)
    {
        Type = "/errors/household-e0002";
        Title = "Household id mismatch with updated household";
        Status = 400;
        Detail = $"The id ({_id}) provided for the household, and the id ({itemId}) provided in the updated household id does not match";
        Instance = $"/{_controllerPath}";
        return this;
    }
}