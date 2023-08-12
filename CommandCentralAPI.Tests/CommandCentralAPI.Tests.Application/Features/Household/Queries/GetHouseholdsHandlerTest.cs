using Application.Contracts.Household;
using Application.Features.Household.Queries.GetAllHouseholds;
using Application.Features.Household.Queries.GetHousehold;
using Application.Features.Household.Shared;
using Application.MappingProfiles;
using AutoMapper;
using CommandCentralAPI.Tests.Application.Mocks;
using FluentAssertions;
using Moq;

namespace CommandCentralAPI.Tests.Application.Features.Household.Queries;

public class GetHouseholdsHandlerTest
{
    private readonly Mock<IHouseholdRepository> _mock;
    private readonly IMapper _mapper;

    public GetHouseholdsHandlerTest()
    {
        _mock = MockHouseholdRepository.GetMockHouseholdRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<HouseholdProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task GetHouseholds()
    {
        // Arrange
        var handler = new GetAllHouseholdsQueryHandler(_mapper, _mock.Object);
        var command = new GetAllHouseholdsQuery();
        
        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.Should().BeOfType<List<HouseholdDetailsDto>>();
    }
}