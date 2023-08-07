namespace Application.Contracts.Identity;

public interface IClaimAuthorizationService
{
    int GetIntegerClaimId(string claim);
}