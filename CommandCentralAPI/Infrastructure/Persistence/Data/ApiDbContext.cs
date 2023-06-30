using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence.Data;

public class ApiDbContext : DbContext
{
    private readonly ILogger<ApiDbContext> _logger;

    public ApiDbContext(DbContextOptions<ApiDbContext> options, ILogger<ApiDbContext> logger) : base(options)
    {
        _logger = logger;
    }
    public DbSet<HouseholdEntity> Household { get; set; }
    public DbSet<GroceryListEntity> GroceryList { get; set; }
    public DbSet<GroceryListItemEntity> GroceryListItem { get; set; }

    private void UpdateDatabase()
    {
        try
        {
            Database.Migrate();
        }
        catch (Exception e)
        {
            _logger.LogCritical("Could not migrate");
        }
    }

    public void InitializeDb()
    {
        while (!Database.CanConnect())
        {
            _logger.LogCritical("Could not connect to database, retry in 1 sec...");
            Thread.Sleep(1000);
        }

        try
        {
            // we only check household since it is essential
            // maybe further should be added 
            Household.Any();
        }
        catch (Exception e)
        {
            // Since Household didnt exist, migrate the latest migration.
            UpdateDatabase();
        }
        
        // Seed dummy data
        if (!Household.Any())
        {
            // Create households
            var household1 = new HouseholdEntity();
            var household2 = new HouseholdEntity();
            Household.Add(household1);
            Household.Add(household2);
            SaveChanges();
            
            // Create grocerylists
            var grocerylist1 = new GroceryListEntity { household_ = household1 };
            var grocerylist2 = new GroceryListEntity { household_ = household2 };
            GroceryList.Add(grocerylist1);
            GroceryList.Add(grocerylist2);
            SaveChanges();
            
            // Create grocery list items
            var item1 = new GroceryListItemEntity { grocery_list_ = grocerylist1, item_name = "Mælk", item_amount = "2 stk"};
            var item2 = new GroceryListItemEntity { grocery_list_ = grocerylist1, item_name = "Kokosnødder", item_amount = "3"};
            var item3 = new GroceryListItemEntity { grocery_list_ = grocerylist2, item_name = "Cykel", item_amount = "1"};
            GroceryListItem.Add(item1);
            GroceryListItem.Add(item2);
            GroceryListItem.Add(item3);
            SaveChanges();
        }
        
    }
}