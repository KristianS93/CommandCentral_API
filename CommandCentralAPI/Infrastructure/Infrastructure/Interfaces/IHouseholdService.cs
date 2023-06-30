using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IHouseholdService
{
    public Task<List<HouseholdEntity>> GetAllAsync();
}