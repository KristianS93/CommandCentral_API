using Domain.Exceptions;
using Infrastructure.Authentication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IGenerateTokenService _generateTokenService;

    public AuthenticationController(IGenerateTokenService generateTokenService)
    {
        _generateTokenService = generateTokenService;
    }

    [HttpPost("{household_id}")]
    public async Task<ActionResult<string>> AuthenticateHouseholdAsync(int household_id)
    {
        try
        {
            return Ok(await _generateTokenService.GenerateToken(household_id));
        }
        catch (HouseholdDoesNotExistException)
        {
            // husk at ændre til korrekt error response.
            return BadRequest();
        }
    }
}