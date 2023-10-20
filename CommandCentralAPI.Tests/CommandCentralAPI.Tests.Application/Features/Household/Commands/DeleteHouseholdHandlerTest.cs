using Application.Contracts.Household;
using Application.Features.Household.Commands.DeleteHousehold;
using Application.Features.Household.Queries.GetAllHouseholds;
using DatabaseFixture;
using MediatR;
using Persistence.Repositories;
using Xunit;
using Xunit.Abstractions;

namespace CommandCentralAPI.Tests.Application.Features.Household.Commands;

public class DeleteHouseholdHandlerTest : IClassFixture<TestDatabaseFixture>
{
    private readonly TestDatabaseFixture _fixture;
    private readonly ITestOutputHelper _testOutputHelper;

    public DeleteHouseholdHandlerTest(ITestOutputHelper testOutputHelper)
    {
        _fixture = new TestDatabaseFixture();
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async void DeleteHousehold_CorrectData_ReturnsUnitValue()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        IHouseholdRepository householdRepository = new HouseholdRepository(context);
        var handler = new DeleteHouseholdCommandHandler(householdRepository);
        var query = new GetAllHouseholdsQuery();
        var householdHandler = new GetAllHouseholdsQueryHandler(householdRepository);
        
        var command = new DeleteHouseholdCommand
        {
            Id = _fixture.SeededHousehold[0].Id
        };
        var expected = new Unit();
        var expected_amount = _fixture.SeededHousehold.Count - 1;
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        var amountOfHouseHolds = (await householdHandler.Handle(query, CancellationToken.None)).Count;
        
        // Assert
        Assert.Equal(expected, result);
        Assert.Equal(expected_amount, amountOfHouseHolds);
    }
    
}