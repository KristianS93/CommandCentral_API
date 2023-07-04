using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public interface IApiDbContext
{
    DbSet<TodoItem> TodoItems { get; set; }
    Task<int> SaveChangesAsync();
}