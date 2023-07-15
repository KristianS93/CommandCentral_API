namespace DatabaseFixture.Tests;

public class Connection: IClassFixture<TestDatabaseFixture>
{
    public TestDatabaseFixture Fixture { get; set; }

    public Connection(TestDatabaseFixture fixture)
    {
        Fixture = fixture;
    }

    [Fact]
    public void CheckConnection()
    {
        using var context = Fixture.CreateContext();
        Assert.True(context.Database.CanConnect());
    }
    
}