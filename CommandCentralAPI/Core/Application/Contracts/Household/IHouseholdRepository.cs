using Application.Contracts.Persistence;
using Domain.Entities.Household;

namespace Application.Contracts.Household;

public interface IHouseholdRepository : IGenericRepository<HouseholdEntity>
{
    Task<IReadOnlyList<HouseholdEntity>> GetAsync();
}