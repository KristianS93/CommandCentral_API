namespace Domain.Models.ErrorResponses;

public class AuthenticationErrors : IErrorResponse
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int? Status { get; set; }
    public string Detail { get; set; }
    public string Instance { get; set; }

    public AuthenticationErrors AccessDenied(string controllerPath)
    {
        Type = "/errors/authentication-e0001";
        Title = "Access denied";
        Status = 401;
        Detail = $"You do not have access to the requested resource.";
        Instance = $"/{controllerPath}";
        return this;
    }
}