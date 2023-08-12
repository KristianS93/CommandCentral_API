// using Application.Contracts.Household;
// using Application.Exceptions;
// using Application.Features.Household.Commands.CreateHousehold;
// using Application.MappingProfiles;
// using AutoMapper;
// using FluentAssertions;
// using Moq;
//
// namespace CommandCentralAPI.Tests.Application.Features.Household.Commands;
//
// public class CreateHouseholdHandlerTest
// {
//     private readonly IMapper _mapper;
//     private readonly Mock<IHouseholdRepository> _householdMock;
//
//     public CreateHouseholdHandlerTest()
//     {
//         // _householdMock = new();
//         var config = new MapperConfiguration(cfg => cfg.AddProfile(typeof(HouseholdProfile)));
//         _mapper = config.CreateMapper();
//     }
//     [Theory]
//     [InlineData("")]
//     public async Task Handle_Should_ReturnFailureResult_WrongHouseholdName(string name)
//     {
//         // Arrange
//         var command = new CreateHouseholdCommand {Name = name, UserId = "UserID"};
//
//         var handler = new CreateHouseholdCommandHandler(_mapper, _householdMock.Object);
//         
//         // Act
//         Action act = async () => await handler.Handle(command, default);
//
//         // Assert
//         act.Should().Throw<BadRequestException>();
//     }
//     
// }