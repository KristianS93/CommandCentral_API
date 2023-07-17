using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Infrastructure.Authentication.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string Generate(HouseholdEntity household)
    {
        var claims = new Claim[]
        {
            // Right now only the household id is needed, in the future a username could be added
            new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
            new Claim("household", household.Id.ToString())
        };

        // Here we create the signing credentials for the jwt security token,
        // we get the bytes based on a secret key string, and we choose the algorithm, to generate the symmetricsecurity key
        // This symmetricsecurity key, should be of AES level encryption, which should suffice, else an RSA key could be generated
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)), SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(1), // the token will live for 1 hour after it was generated, this has to be manipulated in the future. 
            signingCredentials);

        var stringToken = new JwtSecurityTokenHandler().WriteToken(token);
        return stringToken;
    }
}