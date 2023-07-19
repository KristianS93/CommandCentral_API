using System.Text;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions.Options;

public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptions;

    // public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
    // {
    //     _jwtOptions = jwtOptions;
    // }
    
    public JwtBearerOptionsSetup(JwtOptions jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

     public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
        };
    }
    
    public void Configure(string? name, JwtBearerOptions options)
    {
        if (name == Microsoft.Extensions.Options.Options.DefaultName)
        {
            Configure(options);
        }
    }
}