using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace API.Extensions;

public static class MigationExtensions
{
    private static ApiDbContext _dbContext;
    public static void ApplyMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbcontext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
        _dbContext = dbcontext;
        while (!dbcontext.Database.CanConnect())
        {
            Thread.Sleep(1000);
        }

        try
        {
            // we only check household since it is essential
            // maybe further should be added 
            // Household.Any();
            dbcontext.Household.Any();
        }
        catch (Exception e)
        {
            // Since Household didnt exist, migrate the latest migration.
            dbcontext.Database.Migrate();
        }
        SeedDatabase();
    }

    public static void SeedDatabase()
    {
        if (!_dbContext.Household.Any())
        {
            // Todoitems
            var item1 = new TodoItem { Title = "My title", IsCompleted = false };
            var item2 = new TodoItem { Title = "buy milk", IsCompleted = true };
            var item3 = new TodoItem { Title = "go to school", IsCompleted = false };
            _dbContext.TodoItems.Add(item1);
            _dbContext.TodoItems.Add(item2);
            _dbContext.TodoItems.Add(item3);
            _dbContext.SaveChanges();
            
            // add household
            var household1 = new HouseholdEntity { Name = "Kristians hus" };
            var household2 = new HouseholdEntity { Name = "Ibis hus" };
            _dbContext.AddRange(household1, household2);
            _dbContext.SaveChanges();
            
            // add grocerylists
            var grocerylist1 = new GroceryListEntity { HouseholdId = household1.Id };
            var grocerylist2 = new GroceryListEntity { HouseholdId = household2.Id };
            _dbContext.AddRange(grocerylist1, grocerylist2);
            _dbContext.SaveChanges();
            
            // add items to grocerylists
            var gItem1 = new GroceryListItemEntity { ItemName = "Mælk", ItemAmount = 3, GroceryListId = grocerylist1.Id };
            var gItem2 = new GroceryListItemEntity { ItemName = "Toilet papir", ItemAmount = 1, GroceryListId = grocerylist1.Id };
            var gItem3 = new GroceryListItemEntity { ItemName = "Boller", ItemAmount = 5, GroceryListId = grocerylist1.Id };
            var gItem4 = new GroceryListItemEntity { ItemName = "Specialized cykel", ItemAmount = 2, GroceryListId = grocerylist2.Id };
            _dbContext.GroceryListItem.AddRange(gItem1, gItem2, gItem3, gItem4);
            _dbContext.SaveChanges();

        }
    }
}