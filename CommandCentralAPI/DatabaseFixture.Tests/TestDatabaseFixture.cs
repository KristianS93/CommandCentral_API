using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;

namespace DatabaseFixture.Tests;

public class TestDatabaseFixture
{
    private const string ConnectionString = "Host=localhost;Port=5432;Database=commandcentraltest_db;Username=commandcentraltest;Password=commandcentraltestpass;";
    // Tests are run in parallel so a lock is required
    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    public TestDatabaseFixture()
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.Migrate();

                    SeedTestData(context);
                }

                _databaseInitialized = true;
            }
        }
    }

    public ApiDbContext CreateContext()
    {
        return new ApiDbContext(new DbContextOptionsBuilder<ApiDbContext>()
            .UseNpgsql(ConnectionString).Options, new Logger<ApiDbContext>(new LoggerFactory()));
    }

    public void SeedTestData(ApiDbContext context)
    {
        // add household
        var household1 = new HouseholdEntity { Name = "Kristians hus" };
        var household2 = new HouseholdEntity { Name = "Ibis hus" };
        context.AddRange(household1, household2);
        context.SaveChanges();
            
        // add grocerylists
        var grocerylist1 = new GroceryListEntity { HouseholdId = household1.Id };
        var grocerylist2 = new GroceryListEntity { HouseholdId = household2.Id };
        context.AddRange(grocerylist1, grocerylist2);
        context.SaveChanges();
            
        // add items to grocerylists
        var gItem1 = new GroceryListItemEntity { ItemName = "Mælk", ItemAmount = 3, GroceryListId = grocerylist1.Id };
        var gItem2 = new GroceryListItemEntity { ItemName = "Toilet papir", ItemAmount = 1, GroceryListId = grocerylist1.Id };
        var gItem3 = new GroceryListItemEntity { ItemName = "Boller", ItemAmount = 5, GroceryListId = grocerylist1.Id };
        var gItem4 = new GroceryListItemEntity { ItemName = "Specialized cykel", ItemAmount = 2, GroceryListId = grocerylist2.Id };
        context.GroceryListItem.AddRange(gItem1, gItem2, gItem3, gItem4);
        context.SaveChanges();
    }
}
