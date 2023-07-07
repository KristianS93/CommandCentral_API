using API.Controllers;
using Castle.Core.Logging;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace Infrastructure.Tests;

public class HouseholdServiceTests
{
    [Fact]
    public async void GetAllHouseholds()
    {
        // Arrange
        var returnObj = new List<HouseholdEntity> { new HouseholdEntity { Id = 1, Name = "Kristians hus" } };
        var serviceMock = new Mock<IHouseholdService>();
        var loggerMock = Mock.Of<ILogger<HouseholdController>>();
        serviceMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(returnObj);
        var controller = new HouseholdController(loggerMock, serviceMock.Object);
        
        // Act
        var households = await controller.GetHouseholds();
        
        // Assert 
        serviceMock.Verify(r => r.GetAllAsync());
        Assert.Equal(1, returnObj[0].Id);
        Assert.Equal("Kristians hus", returnObj[0].Name);
    }
}