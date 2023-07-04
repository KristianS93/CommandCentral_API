using System.Data.SqlTypes;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence.Data;

public class ApiDbContext : DbContext, IApiDbContext
{
    private readonly ILogger<ApiDbContext> _logger;

    public ApiDbContext(DbContextOptions<ApiDbContext> options, ILogger<ApiDbContext> logger) : base(options)
    {
        _logger = logger;
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     // modelBuilder.Entity<GroceryListItemEntity>()
    //     //     .HasOne<GroceryListEntity>()
    //     //     .WithMany()
    //     //     .HasForeignKey(gli => gli.GroceryListID)
    //     //     .OnDelete(DeleteBehavior.Cascade);
    // }

    // public DbSet<HouseholdEntity> Household { get; set; }
    // public DbSet<GroceryListEntity> GroceryList { get; set; }
    // public DbSet<GroceryListItemEntity> GroceryListItem { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }

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
            // Household.Any();
            TodoItems.Any();
        }
        catch (Exception e)
        {
            // Since Household didnt exist, migrate the latest migration.
            UpdateDatabase();
        }

        // if (!Database.EnsureCreated())
        // {
        //     throw new SqlNullValueException("No db created");
        // }
        
        // Seed dummy data
        // if (!Household.Any())
        if (!TodoItems.Any())
        {
            var item1 = new TodoItem { Title = "My title", IsCompleted = false };
            var item2 = new TodoItem { Title = "buy milk", IsCompleted = true };
            var item3 = new TodoItem { Title = "go to school", IsCompleted = false };
            TodoItems.Add(item1);
            TodoItems.Add(item2);
            TodoItems.Add(item3);
            SaveChanges();
            // // Create households
            // var household1 = new HouseholdEntity { Name = "Kristians hus"};
            // var household2 = new HouseholdEntity { Name = "Ibis hus"} ;
            // Household.Add(household1);
            // Household.Add(household2);
            // SaveChanges();
            //
            // // Create Grocerylists
            // var grocerylist1 = new GroceryListEntity
            // {
            //     HouseholdID = household1.HouseholdID,
            // };
            // var grocerylist2 = new GroceryListEntity
            // {
            //     HouseholdID = household2.HouseholdID,
            // };
            // GroceryList.Add(grocerylist1);
            // GroceryList.Add(grocerylist2);
            // SaveChanges();
            //
            // // Create GroceryListItems
            // var item1 = new GroceryListItemEntity
            // {
            //     GroceryListID = grocerylist1.GroceryListID,
            //     ItemName = "Æbler",
            //     ItemAmount = 4
            // };
            // var item2 = new GroceryListItemEntity
            // {
            //     GroceryListID = grocerylist1.GroceryListID,
            //     ItemName = "Toilet papir",
            //     ItemAmount = 1
            // };
            // var item3 = new GroceryListItemEntity
            // {
            //     GroceryListID = grocerylist2.GroceryListID,
            //     ItemName = "Mælk",
            //     ItemAmount = 2
            // };
            // GroceryListItem.Add(item1);
            // GroceryListItem.Add(item2);
            // GroceryListItem.Add(item3);
            // SaveChanges();

            //     // Create households
            //     var household1 = new HouseholdEntity { Name = "Kristians hus"};
            //     var household2 = new HouseholdEntity { Name = "Ibis hus"} ;
            //     Household.Add(household1);
            //     Household.Add(household2);
            //     SaveChanges();
            //     
            //     // Create grocerylists
            //     var grocerylist1 = new GroceryListEntity { household_ = household1 };
            //     var grocerylist2 = new GroceryListEntity { household_ = household2 };
            //     GroceryList.Add(grocerylist1);
            //     GroceryList.Add(grocerylist2);
            //     SaveChanges();
            //     household1.grocery_list = grocerylist1;
            //     household2.grocery_list = grocerylist2;
            //     SaveChanges();
            //     
            //     // Create grocery list items
            //     var item1 = new GroceryListItemEntity { grocery_list_ = grocerylist1, item_name = "Mælk", item_amount = "2 stk"};
            //     var item2 = new GroceryListItemEntity { grocery_list_ = grocerylist1, item_name = "Kokosnødder", item_amount = "3"};
            //     var item3 = new GroceryListItemEntity { grocery_list_ = grocerylist2, item_name = "Cykel", item_amount = "1"};
            //     GroceryListItem.Add(item1);
            //     GroceryListItem.Add(item2);
            //     GroceryListItem.Add(item3);
            //     SaveChanges();
        }
        
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}