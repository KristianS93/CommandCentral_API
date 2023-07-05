using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface ITodoService
{
    Task<List<TodoItem>> GetAllAsync();
    Task<TodoItem> GetByIdAsync(int id);
    Task CreateAsync(TodoItem todoItem);
    Task UpdateAsync(TodoItem todoItem);
    Task DeleteAsync(TodoItem todoItem);
}