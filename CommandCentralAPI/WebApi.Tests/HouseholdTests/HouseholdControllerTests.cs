using API.Controllers;
using DatabaseFixture.Tests;
using Domain.Entities;
using Infrastructure.Authentication.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
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
        var claimServiceMock = Mock.Of<IClaimAuthorizationService>();
        var loggerMockService= Mock.Of<ILogger<HouseholdService>>();
        var service = new HouseholdService(context, loggerMockService);
        var controller = new HouseholdController(loggerMockController, service, claimServiceMock);
        var expected = new List<HouseholdEntity>
        {
            new HouseholdEntity { Id = 1, Name = "Kristians hus" },
            new HouseholdEntity { Id = 2, Name = "Ibis hus" }
        };
        
        // Act
        var actualHouseholds = await controller.GetHouseholds();
        var actual = actualHouseholds.Result as OkObjectResult;
        // Assert
        Assert.Equivalent(expected, actual.Value);

    }
    
}