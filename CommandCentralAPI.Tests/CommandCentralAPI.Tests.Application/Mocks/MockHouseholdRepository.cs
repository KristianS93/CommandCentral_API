using Application.Contracts.Household;
using Domain.Entities.Household;
using Moq;

namespace CommandCentralAPI.Tests.Application.Mocks;

public class MockHouseholdRepository
{
    public static Mock<IHouseholdRepository> GetMockHouseholdRepository()
    {
        var households = new List<HouseholdEntity>
        {
            new HouseholdEntity
            {
                Id = 1,
                CreatedAt = DateTime.Now,
                LastModified = DateTime.Now,
                Name = "First house"
            },
            new HouseholdEntity
            {
                Id = 2,
                CreatedAt = DateTime.Now,
                LastModified = DateTime.Now,
                Name = "Second house"
            }
            ,
            new HouseholdEntity
            {
                Id = 3,
                CreatedAt = DateTime.Now,
                LastModified = DateTime.Now,
                Name = "Third house"
            }
        };
        var mockRepo = new Mock<IHouseholdRepository>();

        mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(households);

        mockRepo.Setup(r => r.CreateAsync(It.IsAny<HouseholdEntity>())).Returns((HouseholdEntity household) =>
        {
            households.Add(household);
            return Task.FromResult(household);
        });


        return mockRepo;
    }
}