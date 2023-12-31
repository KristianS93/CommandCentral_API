using Application.Contracts.Identity;
using Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
    {
        Console.WriteLine("!!!!!!!!TRYING TO LOG IN!!!!!!!!");
        return Ok(await _authService.Login(request));
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegistrationRequest request)
    {
        return Ok(await _authService.Register(request));
    }
}