using System.Text;
using API.Extensions.Options;
using Infrastructure.Authentication;
using Infrastructure.Authentication.Interfaces;

namespace API.Extensions;

public static class AuthenticationExtension
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection service, ConfigurationManager conf)
    {
        service.AddScoped<IGenerateTokenService, GenerateTokenService>();
        service.AddScoped<IJwtProvider, JwtProvider>();
        service.ConfigureOptions<JwtOptionsSetup>();
        var jwtOptions = new JwtOptions();
        new JwtOptionsSetup(conf).Configure(jwtOptions);

        var set = new JwtBearerOptionsSetup(jwtOptions);
        service.AddAuthentication().AddJwtBearer(o => set.Configure(o));
        
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