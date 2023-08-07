using System.Text;
using Application.Contracts.Identity;
using Application.Models.Identity;
using Identity.DbContext;
using Identity.Models;
using Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Identity;

public static class IdentityServicesRegistration
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        
        // azure specific
        var connectionString =
            Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_AZURE_POSTGRESQL_CONNECTIONSTRING");
        
        // Local development
        if (String.IsNullOrEmpty(connectionString))
        {
            connectionString = configuration.GetConnectionString("Postgres");
        }
        services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });
        
        services.AddIdentity<ApplicationMember, IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUserService, UserService>();
        services.AddScoped<IClaimAuthorizationService, ClaimAuthorizationService>();
        
        services.AddAuthentication(
            options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
        ).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!))
            };
        });

        services.AddAuthorization(options =>
        {
            // Only siteadmin can access siteadmin endpoints
            options.AddPolicy(Permission.SiteAdmin.ToString(), p=> 
                p.RequireRole(Permission.SiteAdmin.ToString()));
            
            // Owner of household, can update and delete household
            // can also be accessed by siteadmins.
            options.AddPolicy(Permission.Owner.ToString(), p => 
                p.RequireRole(Permission.Owner.ToString(), Permission.SiteAdmin.ToString()));
            
            // Member of household, can utilize all basic functionality
            // However they cannot edit the household or delete
            // Endpoints accessible for a member is also accessible for owners, siteadmins
            options.AddPolicy(Permission.Member.ToString(), p => 
                p.RequireRole(Permission.Member.ToString(), Permission.Owner.ToString(), Permission.SiteAdmin.ToString()));
        } );
        
        return services;
    }
}