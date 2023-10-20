using Application.Contracts.Household;
using Application.Features.Household.Queries.GetAllHouseholds;
using Application.Features.Household.Shared;
using DatabaseFixture;
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
    
    // forming mocking test might rework...
    // [Fact]
    // public async Task GetHouseholds_Mock()
    // {
    //     // this in an example on mock without DB!
    //     var householdRepositoryMock = new Mock<IHouseholdRepository>();
    //     // var mapperMock = new Mock<IMapper>();
    //     
    //     
    //     var households = new List<HouseholdEntity>
    //     {
    //         new HouseholdEntity { Name = "Kristians hus" }
    //     };
    //     var expectedDtoList = new List<HouseholdDetailsDto>
    //     {
    //         new HouseholdDetailsDto
    //         {
    //             Id = 1,
    //             CreatedAt = DateTime.Now,
    //             LastModified = DateTime.Now,
    //             Name = "Kristians hus",
    //         }
    //     };
    //     
    //     householdRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(households);
    //     // mapperMock.Setup(mapper => mapper.Map<List<HouseholdDetailsDto>>(households)).Returns(expectedDtoList);
    //     var handler = new GetAllHouseholdsQueryHandler(householdRepositoryMock.Object);
    //     // Act
    //     var query = new GetAllHouseholdsQuery();
    //     var result = await handler.Handle(query, CancellationToken.None);
    //     // Assert
    //     Assert.Equivalent(expectedDtoList, result);
    // }
    
    [Fact]
    public async Task GetHouseholds()
    {
        //This in an example with DB!
        // Arrange
        using var context = _fixture.CreateContext();
        
        // mapper
        // var mapper = _fixture.GetMapper();
        var count = 1;
        var expected = _fixture.SeededHousehold.Select(x => new HouseholdDetailsDto
        {
            Id = count++, // this is a bit annoying, however could be solved by using client side guid generation, instead of db generated ids.
            Name = x.Name,
            CreatedAt = x.CreatedAt,
            LastModified = x.LastModified
        }).ToList();
        
        _householdRepository = new HouseholdRepository(context);
        var handler = new GetAllHouseholdsQueryHandler(_householdRepository);
        
        // Act
        var query = new GetAllHouseholdsQuery();
        var result = await handler.Handle(query, CancellationToken.None);
        // Assert
        Assert.Equivalent(expected, result);
    }
}