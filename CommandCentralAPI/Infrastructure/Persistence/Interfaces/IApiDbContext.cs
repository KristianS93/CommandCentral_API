using Domain.Entities;
using Domain.Entities.Authentication;
using Domain.Entities.MealPlanner;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public interface IApiDbContext
{
    DbSet<TodoItem> TodoItems { get; set; }
    DbSet<MemberEntity> Members { get; set; }
    DbSet<HouseholdEntity> Household { get; set; }
    DbSet<GroceryListEntity> GroceryList { get; set; }
    DbSet<GroceryListItemEntity> GroceryListItem { get; set; }

    DbSet<WeekPlanEntity> WeekPlan { get; set; }

    /// <summary>
    ///
    /// <exception cref="DbUpdateException">An error is encountered while saving to the database.</exception>
    /// <exception cref="DbUpdateConcurrencyException">A concurrency violation is encountered while saving to the database. A concurrency violation occurs when an unexpected number of rows are affected during save. This is usually because the data in the database has been modified since it was loaded into memory.</exception>
    /// <exception cref="OperationCanceledException">If the CancellationToken is canceled.</exception>
    /// </summary>
    /// <returns></returns>
    Task<int> SaveChangesAsync();
}