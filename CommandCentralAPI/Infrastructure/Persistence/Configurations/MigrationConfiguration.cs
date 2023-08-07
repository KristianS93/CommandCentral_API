using Domain.Entities.GroceryList;
using Domain.Entities.Household;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseContext;

namespace Persistence.Configurations;

public static class MigrationConfiguration
{
    private static ApiDbContext? _dbContext;
    public static void ApplyMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var dbcontext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
        _dbContext = dbcontext;
        while (!dbcontext.Database.CanConnect())
        {
            Console.WriteLine("Testing DB");
            Thread.Sleep(1000);
        }
        Console.WriteLine("test Household exist");
        try
        {
            // we only check household since it is essential
            // maybe further should be added 
            // Household.Any();
            dbcontext.Household.Any();
            Console.WriteLine("Household exist");
        }
        catch (Exception)
        {
            // Since Household didnt exist, migrate the latest migration.
            Console.WriteLine("need to migrate");
            dbcontext.Database.Migrate();
            SeedDatabase();
        }
    }

    public static void SeedDatabase()
    {
        SeedHousehold();
    }
    
    public static void SeedHousehold()
    {
        var house1 = new HouseholdEntity
        {
            Name = "Testhus1",
            CreatedAt = DateTime.Now,
            LastModified = DateTime.Now
        };
        var house2 = new HouseholdEntity
        {
            Name = "Testhus2",
            CreatedAt = DateTime.Now,
            LastModified = DateTime.Now
        };
        var house3 = new HouseholdEntity
        {
            Name = "Testhus3",
            CreatedAt = DateTime.Now,
            LastModified = DateTime.Now
        };
        _dbContext.AddRange(house1, house2, house3);
        _dbContext.SaveChanges();

        var groceryList1 = new GroceryListEntity
        {
            CreatedAt = DateTime.Now,
            LastModified = DateTime.Now,
            HouseholdId = house1.Id,
        };

        var groceryList2 = new GroceryListEntity
        {
            CreatedAt = DateTime.Now,
            LastModified = DateTime.Now,
            HouseholdId = house2.Id,
        };

        // var groceryList3 = new GroceryListEntity
        // {
        //     CreatedAt = DateTime.Now,
        //     LastModified = DateTime.Now,
        //     HouseholdId = house3.Id,
        // };
        _dbContext.AddRange(groceryList1, groceryList2);
        _dbContext.SaveChanges();
        
        var gItem1List1 = new GroceryListItemEntity
        {
            CreatedAt = DateTime.Now,
            LastModified = DateTime.Now,
            ItemName = "Mælk",
            ItemAmount = 3,
            GroceryListId = groceryList1.Id,
        };
        var gItem2List1 = new GroceryListItemEntity
        {
            CreatedAt = DateTime.Now,
            LastModified = DateTime.Now,
            ItemName = "Toilet papir",
            ItemAmount = 1,
            GroceryListId = groceryList1.Id,
        };
        var gItem3List1 = new GroceryListItemEntity
        {
            CreatedAt = DateTime.Now,
            LastModified = DateTime.Now,
            ItemName = "Boller",
            ItemAmount = 5,
            GroceryListId = groceryList1.Id,
        };
        var gItem4List2 = new GroceryListItemEntity
        {
            CreatedAt = DateTime.Now,
            LastModified = DateTime.Now,
            ItemName = "Specialized cykel",
            ItemAmount = 2,
            GroceryListId = groceryList2.Id,
        };
        
        _dbContext.AddRange(gItem1List1, gItem2List1, gItem3List1, gItem4List2);
        _dbContext.SaveChanges();
    }
}