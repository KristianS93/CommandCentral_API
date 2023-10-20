// using Application.Features.Household.Commands.CreateHousehold;
// using Application.Features.Household.Commands.UpdateHousehold;
// using Application.Features.Household.Queries.GetHousehold;
// using Application.Features.Household.Shared;
// using AutoMapper;
// using Domain.Entities.Household;
//
// namespace Application.MappingProfiles;
//
// public class HouseholdProfile : Profile
// {
//     public HouseholdProfile()
//     {
//         CreateMap<HouseholdEntity, HouseholdDto>();
//         CreateMap<HouseholdEntity, HouseholdDetailsDto>();
//         CreateMap<UpdateHouseholdCommand, HouseholdEntity>();
//         CreateMap<CreateHouseholdCommand, HouseholdEntity>();
//     }
// }