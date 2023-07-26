using Domain.Entities;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatabaseFixture;

public class TestDatabaseFixture : IDisposable
{
    private string _connectionString;
    // Tests are run in parallel so a lock is required
    private readonly object _lock = new();
    private bool _databaseInitialized;
    private string _databaseName;

    public TestDatabaseFixture()
    {
        _databaseName = "commandcentraltest_db_" + Guid.NewGuid().ToString("N"); 
        _connectionString = $"Host=localhost;Port=5432;Database={_databaseName};Username=commandcentraltest;Password=commandcentraltestpass;";
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
            .UseNpgsql(_connectionString).Options, new Logger<ApiDbContext>(new LoggerFactory()));
    }

    public void SeedTestData(ApiDbContext context)
    {
        // add household
        var household1 = new HouseholdEntity { Name = "Kristians hus" };
        var household2 = new HouseholdEntity { Name = "Ibis hus" };
        var household3 = new HouseholdEntity { Name = "No grocery list" };
        var household4 = new HouseholdEntity { Name = "New house" };
        context.AddRange(household1, household2, household3, household4);
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

    public void Dispose()
    {
        using (var context = CreateContext())
        {
            context.Database.EnsureDeleted();
        }
    }
}
