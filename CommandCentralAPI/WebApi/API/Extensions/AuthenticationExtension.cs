using System.Text;
using API.Extensions.Options;
using Domain.Models.Authentication;
using Infrastructure.Authentication;
using Infrastructure.Authentication.Interfaces;
using Infrastructure.Authentication.MemberAuthentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Extensions;

public static class AuthenticationExtension
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection service, ConfigurationManager conf)
    {
        service.AddScoped<IGenerateTokenService, GenerateTokenService>();
        service.AddScoped<IJwtProvider, JwtProvider>();
        service.AddScoped<IPasswordHasher, PasswordHasher>();
        service.AddScoped<IMemberService, MemberService>();
        service.AddScoped<IClaimAuthorizationService, ClaimAuthorizationService>();
        service.ConfigureOptions<JwtOptionsSetup>();
        
        
        var jwtOptions = new JwtOptions();
        conf.GetSection("JwtOptions").Bind(jwtOptions);
        // new JwtOptionsSetup(conf).Configure(jwtOptions);
        if (String.IsNullOrEmpty(jwtOptions.SecretKey))
        {
            throw new ArgumentException("No secret key in jwt options");
        }
        var set = new JwtBearerOptionsSetup(jwtOptions);
        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o => set.Configure(o));
        
        service.AddAuthorization(o =>
        {
            // SiteAdmin
            o.AddPolicy(Permission.SiteAdmin.ToString(), p => 
                p.RequireRole(Permission.SiteAdmin.ToString()));
            // Household Admin - Future work.
            o.AddPolicy(Permission.HouseholdAdmin.ToString(), p => 
                p.RequireRole(Permission.HouseholdAdmin.ToString(), 
                    Permission.SiteAdmin.ToString()));
            // Member of household
            o.AddPolicy(Permission.Member.ToString(), p => 
                p.RequireRole(Permission.HouseholdAdmin.ToString(), 
                    Permission.SiteAdmin.ToString(), 
                    Permission.Member.ToString()));
        });
        
        return service;
    }
}


// service.ConfigureOptions<JwtBearerOptionsSetup>(); // Would wish
//service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(); // would wish
// xD
// this is hack since I could not make this work.

// service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = jwtOptions.Issuer,
//             ValidAudience = jwtOptions.Audience, 
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)) 
//         };
//     });