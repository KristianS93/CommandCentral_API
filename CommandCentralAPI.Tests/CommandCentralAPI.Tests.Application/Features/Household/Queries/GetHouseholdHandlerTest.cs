using Application.Contracts.Household;
using Application.Features.Household;
using Application.Features.Household.Queries.GetHousehold;
using DatabaseFixture;
using Persistence.Repositories;
using Xunit.Abstractions;

namespace CommandCentralAPI.Tests.Application.Features.Household.Queries;

public class GetHouseholdHandlerTest
{
    private readonly TestDatabaseFixture _fixture;
    private readonly ITestOutputHelper _testOutputHelper;
    public GetHouseholdHandlerTest(ITestOutputHelper testOutputHelper)
    {
        _fixture = new TestDatabaseFixture();
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public async Task Get_Specific_Household()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        
        // mapper
        // var mapper = _fixture.GetMapper();
        // var expected = mapper.Map<HouseholdDto>(_fixture.SeededHousehold[0]);
        var expected = _fixture.SeededHousehold[0].ToDTO();
        
        IHouseholdRepository householdRepository = new HouseholdRepository(context);
        var handler = new GetHouseholdQueryHandler(householdRepository);
        var id = 1;
        // act
        var query = new GetHouseholdQuery(id);
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        Assert.Equivalent(expected, result);
    }
    
    [Fact]
    public async Task Get_Specific_Household_null_object_fails()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        
        // mapper
        // var mapper = _fixture.GetMapper();
        
        IHouseholdRepository householdRepository = new HouseholdRepository(context);
        var handler = new GetHouseholdQueryHandler(householdRepository);
        // act
        var query = new GetHouseholdQuery(23);
        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(query, CancellationToken.None));
    }
    
    
}