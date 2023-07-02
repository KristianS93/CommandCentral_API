using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public interface IApiDbContext
{
    DbSet<HouseholdEntity> Household { get; set; }
    DbSet<GroceryListEntity> GroceryList { get; set; }
    DbSet<GroceryListItemEntity> GroceryListItem { get; set; }
        
    void InitializeDb();
    Task<int> SaveChangesAsync();
}