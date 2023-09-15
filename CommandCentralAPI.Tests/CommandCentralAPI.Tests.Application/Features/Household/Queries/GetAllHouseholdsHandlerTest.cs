using Application.Contracts.Household;
using Application.Features.Household.Queries.GetAllHouseholds;
using Application.Features.Household.Shared;
using AutoMapper;
using DatabaseFixture;
using Domain.Entities.Household;
using Moq;

namespace CommandCentralAPI.Tests.Application.Features.Household.Queries;

public class GetAllHouseholdsHandlerTest : IClassFixture<TestDatabaseFixture>
{
    private readonly TestDatabaseFixture _fixture;

    public GetAllHouseholdsHandlerTest(TestDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetHouseholds()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var mapperMock = new Mock<IMapper>();
        var householdRepositoryMock = new Mock<IHouseholdRepository>();
        
        var households = new List<HouseholdEntity>
        {
            new HouseholdEntity { Name = "Kristians hus" }
        };
        var expectedDtoList = new List<HouseholdDetailsDto>
        {
            new HouseholdDetailsDto
            {
                Id = 1,
                CreatedAt = DateTime.Now,
                LastModified = DateTime.Now,
                Name = "Kristians hus",
            }
        };

        householdRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(households);
        mapperMock.Setup(mapper => mapper.Map<List<HouseholdDetailsDto>>(households)).Returns(expectedDtoList);
        var handler = new GetAllHouseholdsQueryHandler(mapperMock.Object, householdRepositoryMock.Object);
        // Act
        var query = new GetAllHouseholdsQuery();
        var result = await handler.Handle(query, CancellationToken.None);
        // Assert
        Assert.Equal(expectedDtoList, result);
    }
}