using Application.Contracts.Household;
using Domain.Entities.Household;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;

namespace Persistence.Repositories;

public class HouseholdRepository : GenericRepository<HouseholdEntity>, IHouseholdRepository
{
    private readonly ApiDbContext _dbContext;

    public HouseholdRepository(ApiDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IReadOnlyList<HouseholdEntity>> GetAsync()
    {
        return await _dbContext.Set<HouseholdEntity>().AsNoTracking().ToListAsync();
    }
}