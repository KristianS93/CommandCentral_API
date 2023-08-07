using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Contracts.Identity;
using Application.Exceptions;
using Application.Models.Identity;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationMember> _userManager;
    private readonly IOptions<JwtSettings> _jwtSettings;
    private readonly SignInManager<ApplicationMember> _signInManager;

    public AuthService(UserManager<ApplicationMember> userManager, IOptions<JwtSettings> jwtSettings, SignInManager<ApplicationMember> signInManager)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings;
        _signInManager = signInManager;
    }
    public async Task<AuthResponse> Login(AuthRequest request)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == request.UserName);
        // FindByEmailAsync(request.UserName);
        if (user == null)
        {
            throw new NotFoundException($"User with {request.UserName} is not found.", request.UserName);
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (result.Succeeded == false)
        {
            throw new BadRequestException($"Credentials for {request.UserName} are not valid.");
        }
        
        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        var response = new AuthResponse
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName
        };
        
        return response;
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationMember user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Value.Issuer,
            audience: _jwtSettings.Value.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.Value.DurationInMinutes),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        var user = new ApplicationMember
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, Permission.Member.ToString());
            return new RegistrationResponse() { UserId = user.Id };
        }
        StringBuilder str = new StringBuilder();
        foreach (var err in result.Errors)
        {
            str.AppendFormat("- {0}\n", err.Description);
        }
        throw new BadRequestException($"{str}");
    }
}