
using Application.Contracts.Household;
using Application.Features.Household.Commands.UpdateHousehold;
using DatabaseFixture;
using MediatR;
using Persistence.Repositories;
using Xunit.Abstractions;

namespace CommandCentralAPI.Tests.Application.Features.Household.Commands;

public class UpdateHouseholdHandlerTest : IClassFixture<TestDatabaseFixture>
{
    private readonly TestDatabaseFixture _fixture;
    private readonly ITestOutputHelper _testOutputHelper;
    
    public UpdateHouseholdHandlerTest(ITestOutputHelper testOutputHelper)
    {
        _fixture = new TestDatabaseFixture();
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async void UpdateHousehold_CorrectData_ReturnsUnitValue()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        IHouseholdRepository householdRepository = new HouseholdRepository(context);
        var handler = new UpdateHouseholdCommandHandler(householdRepository);
        var household = _fixture.SeededHousehold[0];
        var command = new UpdateHouseholdCommand
        {
            Id = household.Id,
            Name = household.Name
        };

        var expected = new Unit();
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.Equal(expected, result);
    }
}