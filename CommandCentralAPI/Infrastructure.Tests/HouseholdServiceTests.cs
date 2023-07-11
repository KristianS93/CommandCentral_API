using API.Controllers;
using DatabaseFixture.Tests;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Infrastructure.Tests;

public class HouseholdServiceTests : IClassFixture<TestDatabaseFixture>
{
    private Mock<IHouseholdService> _serviceMock;
    private ILogger<HouseholdService> _loggerMock;
    public TestDatabaseFixture Fixture { get; set; }
    public HouseholdServiceTests(TestDatabaseFixture fixture)
    {
        _serviceMock = new Mock<IHouseholdService>();
        _loggerMock = Mock.Of<ILogger<HouseholdService>>();
        Fixture = fixture;
    }
    
    // [Fact]
    // public async void GetAllHouseholds()
    // {
    //     // Arrange
    //     var returnObj = new List<HouseholdEntity> { new HouseholdEntity { Id = 1, Name = "Kristians hus" } };
    //     _serviceMock.Setup(r => r.GetAllAsync())
    //         .ReturnsAsync(returnObj);
    //     var controller = new HouseholdController(_loggerMock, _serviceMock.Object);
    //     
    //     // Act
    //     var households = await controller.GetHouseholds();
    //     
    //     // Assert 
    //     _serviceMock.Verify(r => r.GetAllAsync());
    //     Assert.Equal(1, returnObj[0].Id);
    //     Assert.Equal("Kristians hus", returnObj[0].Name);
    // }

    [Fact]
    public async void GetHouseholdById()
    {
        // Arrange
        using var context = Fixture.CreateContext();
        var service = new HouseholdService(context, _loggerMock);
        var expected = new HouseholdEntity { Id = 1, Name = "Kristians hus" };
        
        // Act
        var actual = await service.GetByIdAsync(1);
        
        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public async void GetHouseholdbyId_Return_error()
    {
        // Arrange
        using var context = Fixture.CreateContext();
        var service = new HouseholdService(context, _loggerMock);
        
        // Assert
        await Assert.ThrowsAsync<HouseholdDoesNotExistException>(() => service.GetByIdAsync(5));
    }

    [Theory]
    [InlineData("House name")]
    [InlineData("1221 my HOuse !?")]
    public async void CreateAsync(string name)
    {
        // Arrange
        using var context = Fixture.CreateContext();
        var service = new HouseholdService(context, _loggerMock);
        var expected = new HouseholdEntity { Name = name };
        
        // Act
        var actual = await service.CreateAsync(name);
        
        // Assert
        Assert.Equal(expected.Name, actual.Name);
    }

    [Theory]
    [InlineData("      ")]
    [InlineData("   !   ")]
    [InlineData("2342")]
    [InlineData("23 42")]
    public async void CreateAsync_Validation_ReturnsException(string name)
    {
        // Arrange
        using var context = Fixture.CreateContext();
        var service = new HouseholdService(context, _loggerMock);
        var expected = new HouseholdEntity { Name = name };
        
        // Act + Assert
        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(name));
    }
}