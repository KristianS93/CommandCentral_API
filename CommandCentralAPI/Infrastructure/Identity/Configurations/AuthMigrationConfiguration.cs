using Identity.DbContext;
using Identity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Configurations;

public static class AuthMigrationConfiguration
{
    private static AuthDbContext? _dbContext;
    public static void ApplyAuthMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var dbcontext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
        _dbContext = dbcontext;
        while (!dbcontext.Database.CanConnect())
        {
            Console.WriteLine("Testing DB");
            Thread.Sleep(1000);
        }
        if (dbcontext.Database.CanConnect())
        {
            dbcontext.Database.Migrate();
            SeedDatabase();
        }
    }

    public static void SeedDatabase()
    {
        RoleConfiguration();
        UserSeed();
        UserRoleSeed();
    }

    public static void UserSeed()
    {
        var hasher = new PasswordHasher<ApplicationMember>();
        var admin = new ApplicationMember
        {
            Id = "7c1589ea-3df1-405d-b05a-52e29d66840b",
            Email = "admin@localhost.com",
            NormalizedEmail = "admin@localhost.com".ToUpper(),
            FirstName = "System",
            LastName = "Admin",
            UserName = "webadmin",
            NormalizedUserName = "WEBADMIN",
            PasswordHash = hasher.HashPassword(null, "password"),
            EmailConfirmed = true,
            HouseholdId = 1,
            GroceryListId = 1
        };
        var user = new ApplicationMember
        {
            Id = "397b23e4-1a1d-4cb6-95f6-192488217a83",
            Email = "user@localhost.com",
            NormalizedEmail = "user@localhost.com".ToUpper(),
            FirstName = "System",
            LastName = "User",
            UserName = "webuser",
            NormalizedUserName = "WEBUSER",
            PasswordHash = hasher.HashPassword(null, "userpassword"),
            EmailConfirmed = true,
            HouseholdId = 2,
            GroceryListId = 2
        };
        _dbContext.AddRange(admin, user);
        _dbContext.SaveChanges();
    }

    public static void UserRoleSeed()
    {
        var userRole = new IdentityUserRole<string>
        {
            // Member
            RoleId = "a2f39caf-0eff-4dda-a411-64ae003b17ea",
            // User
            UserId = "397b23e4-1a1d-4cb6-95f6-192488217a83"
        };
        var adminRole = new IdentityUserRole<string>
        {
            // SiteAdmin
            RoleId = "e0b0bc42-23e9-4771-bf9e-6450393a124b",
            // Admin
            UserId = "7c1589ea-3df1-405d-b05a-52e29d66840b"
        };
        _dbContext.AddRange(userRole, adminRole);
        _dbContext.SaveChanges();
    }
    public static void RoleConfiguration()
    {
        var memberIdentity = new IdentityRole
        {
            Id = "a2f39caf-0eff-4dda-a411-64ae003b17ea",
            Name = Permission.Member.ToString(),
            NormalizedName = Permission.Member.ToString().ToUpper()
        };
        
        var ownerIdentity = new IdentityRole
        {
            Id = "5db7187c-1ebb-4072-aca9-39b5b5048656",
            Name = Permission.Owner.ToString(),
            NormalizedName = Permission.Owner.ToString().ToUpper()
        };
        var siteAdminIdentity = new IdentityRole
        {
            Id = "e0b0bc42-23e9-4771-bf9e-6450393a124b",
            Name = Permission.SiteAdmin.ToString(),
            NormalizedName = Permission.SiteAdmin.ToString().ToUpper()
        };
        _dbContext.AddRange(memberIdentity, ownerIdentity, siteAdminIdentity);
        _dbContext.SaveChanges();
    }
}