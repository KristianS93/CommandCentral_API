using DatabaseFixture.Tests;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Exceptions.GroceryList;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using Xunit.Abstractions;

namespace Infrastructure.Tests;

public class GroceryListTests : IClassFixture<TestDatabaseFixture>
{
    public TestDatabaseFixture Fixture { get; set; }
    private ILogger<GroceryListService> _loggerMock;
    private readonly ITestOutputHelper output;

    public GroceryListTests(TestDatabaseFixture fixture, ITestOutputHelper _output)
    {
        _loggerMock = Mock.Of<ILogger<GroceryListService>>();
        Fixture = fixture;
        output = _output;
    }

    [Fact]
    public async void GetGroceryListByHouseholdId()
    {
        // Arrange
        var householdId = 2;
        using var context = Fixture.CreateContext();
        var service = new GroceryListService(context, _loggerMock);
        
        var expected = new GroceryListEntity { Id = 2, HouseholdId = householdId };
        var gItem4 = new GroceryListItemEntity {GroceryList = expected, GroceryListItemId = 4, ItemName = "Specialized cykel", ItemAmount = 2, GroceryListId = 2 };
        expected.GroceryListItems = new List<GroceryListItemEntity> { gItem4 };
        var expectedItems = expected.GroceryListItems.ToList()[0];
        
        // Act
        var actual = await service.GetAsyncByHouseholdIdAsync(householdId);
        var actualItems = actual.GroceryListItems.ToList()[0];
        
        // Assert
        Assert.Equal(expected.HouseholdId,actual.HouseholdId);
        Assert.Equal(expected.Id, actual.Id);
        
        Assert.Equal(expectedItems.GroceryListItemId, actualItems.GroceryListItemId);
        Assert.Equal(expectedItems.ItemAmount, actualItems.ItemAmount);
        Assert.Equal(expectedItems.ItemName, actualItems.ItemName);
        Assert.Equal(expectedItems.GroceryListId, actualItems.GroceryListId);
    }

    [Fact]
    public async void GetGroceryList_wrong_id()
    {
        // Arrange
        var householdId = 10;
        using var context = Fixture.CreateContext();
        var service = new GroceryListService(context, _loggerMock);
        
        // Act + Assert
       await Assert.ThrowsAsync<GroceryListDoesNotExistException>(() => service.GetAsyncByHouseholdIdAsync(householdId));
    }

    [Fact]
    public async void CreateGroceryList()
    {
        // Arrange
        var householdId = 3;
        using var context = Fixture.CreateContext();
        var service = new GroceryListService(context, _loggerMock);
        var expectedId = 3;
        
        // Act
        await service.CreateAsync(householdId);
        var actual = context.GroceryList.FirstOrDefault(e => e.HouseholdId == householdId);
        
        //Assert
        Assert.Equal(expectedId, actual.Id);

    }

    [Fact]
    public async void CreateGroceryList_HouseholdDoesNotExist()
    {
        // Arrange
        var householdId = 15;
        using var context = Fixture.CreateContext();
        var service = new GroceryListService(context, _loggerMock);
        
        // Act + Assert
        await Assert.ThrowsAsync<HouseholdDoesNotExistException>(() => service.CreateAsync(householdId));
    }
    
    [Fact]
    public async void CreateGroceryList_AlreadyHaveGroceryList()
    {
        // Arrange
        var householdId = 2;
        using var context = Fixture.CreateContext();
        var service = new GroceryListService(context, _loggerMock);
        
        // Act + Assert
        await Assert.ThrowsAsync<GroceryListDuplicateException>(() => service.CreateAsync(householdId));
    }

    [Fact]
    public async void DeleteGroceryListByHouseholdId()
    {
        // Arrange
        var householdId = 2;
        using var context = Fixture.CreateContext();
        var service = new GroceryListService(context, _loggerMock);
        
        // Act
        await service.DeleteAsync(householdId);

        // Assert
        // GroceryListItem also has to be checked because of the cascade delete behavior
        Assert.Null(context.GroceryList.Find(2));
        Assert.Null(context.GroceryListItem.FirstOrDefault(e => e.GroceryListId == 2));
    }

    [Fact]
    public async void DeleteGroceryList_Wrong_GroceryList()
    {
        // Arrange
        var householdId = 15;
        using var context = Fixture.CreateContext();
        var service = new GroceryListService(context, _loggerMock);
        
        // Act + Assert
        await Assert.ThrowsAsync<GroceryListDoesNotExistException>(() => service.DeleteAsync(householdId));
    }
}