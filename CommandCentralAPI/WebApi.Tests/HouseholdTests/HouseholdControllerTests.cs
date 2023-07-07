using API.Controllers;
using Domain.Entities;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace WebApi.Tests.HouseholdTests;

public class HouseholdControllerTests : IClassFixture<TestDatabaseFixture>
{
    public HouseholdControllerTests(TestDatabaseFixture fixture)
    {
        Fixture = fixture;
    }
    public TestDatabaseFixture Fixture { get; set; }

    [Fact]
    public async void GetHouseholds_ListOfIds()
    {
        // Arrange 
        using var context = Fixture.CreateContext();
        var loggerMockController = Mock.Of<ILogger<HouseholdController>>();
        var loggerMockService= Mock.Of<ILogger<HouseholdService>>();
        var service = new HouseholdService(context, loggerMockService);
        var controller = new HouseholdController(loggerMockController, service);
        var expected = new List<HouseholdEntity>
        {
            new HouseholdEntity { Id = 1, Name = "Kristians hus" },
            new HouseholdEntity { Id = 2, Name = "Ibis hus" }
        };
        
        // Act
        var actualHouseholds = await controller.GetHouseholds();
        
        // Assert
        Assert.Equivalent(expected, actualHouseholds);
        
    }
    
}