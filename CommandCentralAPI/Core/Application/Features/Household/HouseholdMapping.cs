using Application.Features.Household.Commands.CreateHousehold;
using Application.Features.Household.Commands.UpdateHousehold;
using Application.Features.Household.Queries.GetHousehold;
using Application.Features.Household.Shared;
using Domain.Entities.Household;

namespace Application.Features.Household;

public static class HouseholdMapping
{
    public static HouseholdDto ToDTO(this HouseholdEntity entity)
    {
        return new HouseholdDto { Name = entity.Name };
    }

    public static HouseholdDetailsDto ToDetailsDTO(this HouseholdEntity entity)
    {
        return new HouseholdDetailsDto
        {
            Id = entity.Id,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt,
            LastModified = entity.LastModified
        };
    }
    public static List<HouseholdDetailsDto> ToListOfDetailsDTO(this IReadOnlyList<HouseholdEntity> ents)
    {
        var result = new List<HouseholdDetailsDto>();
        for (int i = 0; i < ents.Count; i++)
        {
            result.Add(
                new HouseholdDetailsDto
                {
                    Id = ents[i].Id,
                    Name = ents[i].Name,
                    CreatedAt = ents[i].CreatedAt,
                    LastModified = ents[i].LastModified
                });
        }

        return result;
    }

    public static HouseholdEntity ToHouseholdEntity(this CreateHouseholdCommand command)
    {
        return new HouseholdEntity
        {
            Name = command.Name
        };
    }

    public static HouseholdEntity UpdateEntity(this UpdateHouseholdCommand command, HouseholdEntity entity)
    {
        return new HouseholdEntity
        {
            Id = entity.Id,
            Name = command.Name,
            CreatedAt = entity.CreatedAt,
            LastModified = entity.LastModified
        };
    }
}