using System.Security.Authentication;
using Infrastructure.Authentication.Interfaces;

namespace Infrastructure.Authentication;

public class ClaimAuthorizationService : IClaimAuthorizationService
{
    public void ConfirmHouseholdClaim(string claim, int householdId)
    {
        if (Convert.ToInt32(claim) != householdId)
        {
            throw new AuthenticationException();
        }
    }
}