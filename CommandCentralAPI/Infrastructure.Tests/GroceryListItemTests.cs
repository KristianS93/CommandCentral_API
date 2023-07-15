using DatabaseFixture.Tests;
using Domain.Entities;
using Domain.Exceptions.GroceryList;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Infrastructure.Tests;

public class GroceryListItemTests : IClassFixture<TestDatabaseFixture>
{
    public TestDatabaseFixture Fixture { get; set; }
    private ILogger<GroceryListItemService> _loggerMock;

    public GroceryListItemTests(TestDatabaseFixture fixture)
    {
        _loggerMock = Mock.Of<ILogger<GroceryListItemService>>();
        Fixture = fixture;
    }

    [Fact]
    public async void GetGroceryListItem()
    {
        // Arrange
        var groceryListItemId = 1;
        using var context = Fixture.CreateContext();
        var service = new GroceryListItemService(context, _loggerMock);
        var expected = new GroceryListItemEntity { GroceryListItemId = 1, ItemName = "Mælk", ItemAmount = 3, GroceryListId = 1 };
        
        // Act
        var actual = await service.GetByIdAsync(groceryListItemId);
    
        // Arrange
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public async void GetGroceryListItem_Wrong_Id()
    {
        // Arrange
        var groceryListItemId = 143;
        using var context = Fixture.CreateContext();
        var service = new GroceryListItemService(context, _loggerMock);
        
        // Act + Assert
        await Assert.ThrowsAsync<ItemDoesNotExistException>(() => service.GetByIdAsync(groceryListItemId));
    }

    [Fact]
    public async void CreateGroceryListItem()
    {
        // Arrange
        var groceryListId = 1;
        var itemName = "Pasta med dolmio";
        var itemAmount = 3;
        using var context = Fixture.CreateContext();
        var service = new GroceryListItemService(context, _loggerMock);
        
        // Act
        var expected = new GroceryListItemEntity { GroceryListItemId = 5, GroceryListId = groceryListId, GroceryList = null, ItemName = itemName, ItemAmount = itemAmount};
        await service.CreateAsync(expected);
        var actual = context.GroceryListItem.Find(5);

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Theory]
    [InlineData("1908329183")]
    [InlineData("!€#€#%# ")]
    [InlineData("     ")]
    public async void CreateGroceryListItem_Non_Allowed_ItemNames(string itemName)
    {
        // Arrange
        using var context = Fixture.CreateContext();
        var service = new GroceryListItemService(context, _loggerMock);

        var item = new GroceryListItemEntity
            { GroceryListItemId = 5, GroceryList = null, ItemName = itemName, ItemAmount = 2, GroceryListId = 1 };
        
        // Act + Assert
        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(item));
    }

    [Fact]
    public async void CreateGroceryListItem_GroceryListDoesNotExist()
    {
        // Arrange
        var groceryListId = 21;
        using var context = Fixture.CreateContext();
        var service = new GroceryListItemService(context, _loggerMock);
        
        // Act + Assert
        var item = new GroceryListItemEntity
            { GroceryListItemId = 5, GroceryList = null, ItemName = "mælk", ItemAmount = 2, GroceryListId = groceryListId };
        await Assert.ThrowsAsync<GroceryListDoesNotExistException>(() => service.CreateAsync(item));
    }

    [Fact]
    public async void UpdateGroceryListItem()
    {
        // Arrange
        using var context = Fixture.CreateContext();
        var service = new GroceryListItemService(context, _loggerMock);
        var expected = new GroceryListItemEntity { GroceryListItemId = 1, GroceryListId = 1, GroceryList = null, ItemName = "Monster", ItemAmount = 3};

        // Act
        await service.UpdateAsync(expected);
        var actual = context.GroceryListItem.Find(1);
        
        // this is bad practice, but this is needed to avoid the circular reference to the grocery list.
        actual!.GroceryList = null;
        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public async void UpdateGroceryListItem_GroceryListDoesNotExist()
    {
        // Arrange
        using var context = Fixture.CreateContext();
        var service = new GroceryListItemService(context, _loggerMock);
        var expected = new GroceryListItemEntity { GroceryListItemId = 1, GroceryListId = 25, GroceryList = null, ItemName = "Monster", ItemAmount = 3};
        
        // Act + Assert
        await Assert.ThrowsAsync<GroceryListDoesNotExistException>(() => service.UpdateAsync(expected));
    }

    [Fact]
    public async void DeleteGroceryListItem()
    {
        // Arrange
        using var context = Fixture.CreateContext();
        var service = new GroceryListItemService(context, _loggerMock);
        var itemId = 3;
        
        // Act
        await service.DeleteAsync(itemId);
        
        // Assert
        Assert.Null(context.GroceryListItem.Find(itemId));
    }
    
    [Fact]
    public async void DeleteGroceryListItem_ItemDoesNotExist()
    {
        // Arrange
        using var context = Fixture.CreateContext();
        var service = new GroceryListItemService(context, _loggerMock);
        
        // Act + Assert
        await Assert.ThrowsAsync<ItemDoesNotExistException>(() => service.DeleteAsync(635));
    }

}