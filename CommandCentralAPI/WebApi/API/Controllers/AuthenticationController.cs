using Domain.Entities.Authentication;
using Domain.Exceptions;
using Infrastructure.Authentication.Interfaces;
using Infrastructure.Authentication.MemberAuthentication;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IMemberService _memberService;

    public AuthenticationController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> AuthenticateHouseholdAsync(MemberEntity member)
    {
        try
        {
            return Ok(await _memberService.LoginReturnTokenAsync(member));
        }
        catch (HouseholdDoesNotExistException)
        {
            // husk at ændre til korrekt error response.
            return BadRequest();
        }
    }
}