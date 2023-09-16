using Application.Contracts.Household;
using Application.Features.Household.Queries.GetAllHouseholds;
using Application.Features.Household.Shared;
using Application.MappingProfiles;
using AutoMapper;
using DatabaseFixture;
using Domain.Entities.Household;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence.Repositories;
using Xunit.Abstractions;

namespace CommandCentralAPI.Tests.Application.Features.Household.Queries;

public class GetAllHouseholdsHandlerTest : IClassFixture<TestDatabaseFixture>
{
    private readonly TestDatabaseFixture _fixture;
    private readonly ITestOutputHelper _testOutputHelper;
    public IHouseholdRepository _householdRepository;

    public GetAllHouseholdsHandlerTest(TestDatabaseFixture fixture, ITestOutputHelper testOutputHelper)
    {
        _fixture = fixture;
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task GetHouseholds_Mock()
    {
        // this in an example on mock without DB!
        var householdRepositoryMock = new Mock<IHouseholdRepository>();
        var mapperMock = new Mock<IMapper>();
        
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
        Assert.Equivalent(expectedDtoList, result);
    }
    
    [Fact]
    public async Task GetHouseholds()
    {
        //This in an example with DB!
        // Arrange
        using var context = _fixture.CreateContext();
        
        // mapper
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<HouseholdProfile>();
        });
        var count = 1;
        var expected = _fixture.SeededHouseholds.Select(x => new HouseholdDetailsDto
        {
            Id = count++, // this is a bit annoying, however could be solved by using client side guid generation, instead of db generated ids.
            Name = x.Name,
            CreatedAt = x.CreatedAt,
            LastModified = x.LastModified
        }).ToList();
        
        var mapper = mapperConfig.CreateMapper();
        
        _householdRepository = new HouseholdRepository(context);
        var handler = new GetAllHouseholdsQueryHandler(mapper, _householdRepository);
        
        // Act
        var query = new GetAllHouseholdsQuery();
        var result = await handler.Handle(query, CancellationToken.None);
        // Assert
        Assert.Equivalent(expected, result);
    }
}