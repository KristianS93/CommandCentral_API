using Microsoft.AspNetCore.Authorization;

namespace Identity.Models;

public class CustomAuthorize : AuthorizeAttribute
{
    public CustomAuthorize(Permission permission) : base(policy: permission.ToString())
    {
        
    }
}