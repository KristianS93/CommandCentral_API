using Microsoft.EntityFrameworkCore;

namespace DatabaseFixture.Tests;

public class ConnectionTests: IClassFixture<TestDatabaseFixture>
{
    public TestDatabaseFixture Fixture { get; set; }

    public ConnectionTests(TestDatabaseFixture fixture)
    {
        Fixture = fixture;
    }

    [Fact]
    public void CheckConnection()
    {
        using var context = Fixture.CreateContext();
        Assert.True(context.Database.CanConnect());
    }

    [Fact]
    public async void CheckHouseholdSeed()
    {
        using var context = Fixture.CreateContext();

        var first = await context.Household.FirstOrDefaultAsync();
        Assert.NotNull(first);
    }
    
    [Fact]
    public async void CheckGroceryListSeed()
    {
        using var context = Fixture.CreateContext();

        var first = await context.GroceryList.FirstOrDefaultAsync();
        Assert.NotNull(first);
    }
    
    [Fact]
    public async void CheckGroceryListItemSeed()
    {
        using var context = Fixture.CreateContext();

        var first = await context.GroceryListItem.FirstOrDefaultAsync();
        Assert.NotNull(first);
    }
    
}