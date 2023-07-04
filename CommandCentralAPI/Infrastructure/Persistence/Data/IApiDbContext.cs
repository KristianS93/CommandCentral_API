using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public interface IApiDbContext
{
    DbSet<TodoItem> TodoItems { get; set; }
    DbSet<HouseholdEntity> Household { get; set; }
    DbSet<GroceryListEntity> GroceryList { get; set; }
    Task<int> SaveChangesAsync();
}