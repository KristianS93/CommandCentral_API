// using DatabaseFixture;
// using Domain.Entities;
// using Domain.Exceptions;
// using Infrastructure.Services;
// using Microsoft.Extensions.Logging;
// using Moq;
//
// namespace CommandCentralApi.Tests.Infrastructure;
//
// public class HouseholdServiceTests : IClassFixture<TestDatabaseFixture>
// {
//     private ILogger<HouseholdService> _loggerMock;
//     public TestDatabaseFixture Fixture { get; set; }
//     public HouseholdServiceTests(TestDatabaseFixture fixture)
//     {
//         _loggerMock = Mock.Of<ILogger<HouseholdService>>();
//         Fixture = fixture;
//     }
//     
//     // [Fact]
//     // public async void GetAllHouseholds()
//     // {
//     //     // Arrange
//     //     var returnObj = new List<HouseholdEntity> { new HouseholdEntity { Id = 1, Name = "Kristians hus" } };
//     //     _serviceMock.Setup(r => r.GetAllAsync())
//     //         .ReturnsAsync(returnObj);
//     //     var controller = new HouseholdController(_loggerMock, _serviceMock.Object);
//     //     
//     //     // Act
//     //     var households = await controller.GetHouseholds();
//     //     
//     //     // Assert 
//     //     _serviceMock.Verify(r => r.GetAllAsync());
//     //     Assert.Equal(1, returnObj[0].Id);
//     //     Assert.Equal("Kristians hus", returnObj[0].Name);
//     // }
//
//     [Fact]
//     public async void GetHouseholdById()
//     {
//         // Arrange
//         using var context = Fixture.CreateContext();
//         var service = new HouseholdService(context, _loggerMock);
//         var expected = new HouseholdEntity { Id = 4, Name = "New house" };
//         
//         // Act
//         var actual = await service.GetByIdAsync(4);
//         
//         // Assert
//         Assert.Equivalent(expected, actual);
//     }
//
//     [Fact]
//     public async void GetHouseholdbyId_Return_error()
//     {
//         // Arrange
//         using var context = Fixture.CreateContext();
//         var service = new HouseholdService(context, _loggerMock);
//         
//         // Assert
//         await Assert.ThrowsAsync<HouseholdDoesNotExistException>(() => service.GetByIdAsync(1000));
//     }
//
//     [Theory]
//     [InlineData("House name")]
//     [InlineData("1221 my HOuse !?")]
//     public async void CreateAsync(string name)
//     {
//         // Arrange
//         using var context = Fixture.CreateContext();
//         var service = new HouseholdService(context, _loggerMock);
//         var expected = new HouseholdEntity { Name = name };
//         
//         // Act
//         var actual = await service.CreateAsync(name);
//         
//         // Assert
//         Assert.Equal(expected.Name, actual.Name);
//     }
//
//     [Theory]
//     [InlineData("      ")]
//     [InlineData("   !   ")]
//     [InlineData("2342")]
//     [InlineData("23 42")]
//     public async void CreateAsync_Validation_ReturnsException(string name)
//     {
//         // Arrange
//         using var context = Fixture.CreateContext();
//         var service = new HouseholdService(context, _loggerMock);
//         var expected = new HouseholdEntity { Name = name };
//         
//         // Act + Assert
//         await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(name));
//     }
//
//     [Fact]
//     public async void UpdateAsync()
//     {
//         // Arrange
//         using var context = Fixture.CreateContext();
//         var service = new HouseholdService(context, _loggerMock);
//         var expected = await context.Household.FindAsync(2);
//         
//         // Act
//         expected!.Name = "New household name";
//         var actual = await context.Household.FindAsync(2);
//         
//         // Assert
//         Assert.Equivalent(expected, actual);
//     }
//     
//     [Fact]
//     public async void Deletehousehold_Wrong_Id()
//     {
//         // Arrange
//         using var context = Fixture.CreateContext();
//         var service = new HouseholdService(context, _loggerMock);
//
//         await Assert.ThrowsAsync<HouseholdDoesNotExistException>(() => service.DeleteAsync(2522));
//     }
//
//     [Fact]
//     public async void DeleteHousehold()
//     {
//         // Arrange
//         var id = 1;
//         using var context = Fixture.CreateContext();
//         var service = new HouseholdService(context, _loggerMock);
//         var groceryListId = context.GroceryList.Where(h => h.HouseholdId == id).FirstOrDefault();
//
//         
//         // Act
//         await service.DeleteAsync(id);
//         
//
//         // Assert
//         // multiple things have to be checked as this delete function will cascade delete
//         // grocery list and items 
//         
//         // Check household is deleted 
//         Assert.Null(await context.Household.FindAsync(id));
//         // Check grocerylist is deleted
//         Assert.Empty(context.GroceryList.Where(i => i.HouseholdId == id));
//         // Check grocerylist items is deleted, same id can be used here since the first grocery list created is id 1
//         Assert.Empty(context.GroceryListItem.Where(i => i.GroceryListId == groceryListId.Id));
//     }
// }