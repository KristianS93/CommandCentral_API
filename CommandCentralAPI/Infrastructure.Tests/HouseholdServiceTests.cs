using Domain.Entities;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Persistence.Data;

namespace Infrastructure.Tests;

public class HouseholdServiceTests
{
    private HouseholdService _householdService;
    private Mock<IApiDbContext> _dbContextMock;

    public HouseholdServiceTests()
    {
        _dbContextMock = new Mock<IApiDbContext>();
        _householdService = new HouseholdService(_dbContextMock.Object, null);
        
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllHouseHolds()
    {
        // // Arrange
        // var expectedHouseholds = new List<HouseholdEntity>
        // {
        //     new HouseholdEntity { Id = 1, Name = "House1" },
        //     new HouseholdEntity { Id = 2, Name = "House2" },
        // };
        // // _dbContextMock.Setup(db => db.Household.ToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expectedHouseholds);
        // _dbContextMock.Setup(db => db.Household.ToListAsync(It.IsAny<CancellationToken>()))
        //     .ReturnsAsync(expectedHouseholds);
        // // Act
        // var actualHousehold = await _householdService.GetAllAsync();
        //
        // // Assert
        // Assert.Equal(expectedHouseholds, actualHousehold);
    }
}