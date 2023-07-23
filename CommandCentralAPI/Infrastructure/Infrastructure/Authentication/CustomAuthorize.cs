using Domain.Models.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication;

public class CustomAuthorize : AuthorizeAttribute
{
    public CustomAuthorize(Permission permission) : base(policy: permission.ToString())
    {
        
    }
}