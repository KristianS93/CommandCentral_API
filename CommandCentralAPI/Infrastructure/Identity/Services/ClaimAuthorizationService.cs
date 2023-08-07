using System.Security.Authentication;
using Application.Contracts.Identity;

namespace Identity.Services;

public class ClaimAuthorizationService : IClaimAuthorizationService
{
    public int GetIntegerClaimId(string claim)
    {
        try
        {
            return Convert.ToInt32(claim);
        
        } catch (Exception){
            throw new AuthenticationException();
        }
        
    }
}