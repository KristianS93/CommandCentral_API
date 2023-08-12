using DatabaseFixture;

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
        var command = 
        // Act
        
        // Assert
        
    }
}