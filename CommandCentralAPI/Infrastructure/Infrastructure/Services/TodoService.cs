using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Infrastructure.Services;

public class TodoService: ITodoService
{
    private readonly IApiDbContext _dbContext;
    
    public TodoService(IApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<TodoItem>> GetAllAsync()
    {
        return await _dbContext.TodoItems.ToListAsync();
    }

    public async Task<TodoItem> GetByIdAsync(int id)
    {
        return await _dbContext.TodoItems.FindAsync(id);
    }

    public async Task CreateAsync(TodoItem todoItem)
    {
        _dbContext.TodoItems.Add(todoItem);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TodoItem todoItem)
    {
        _dbContext.TodoItems.Update(todoItem);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TodoItem todoItem)
    {
        _dbContext.TodoItems.Remove(todoItem);
        await _dbContext.SaveChangesAsync();
    }
}