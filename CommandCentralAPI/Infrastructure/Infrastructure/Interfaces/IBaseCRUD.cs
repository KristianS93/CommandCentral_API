namespace Infrastructure.Interfaces;

public interface BaseCRUD<T>
{
    public Task<List<T>> GetAllAsync();
    public Task<T> GetByIdAsync(int id);
    public Task CreateAsync(T item);
    public Task UpdateAsync(T item);
    public Task DeleteAsync(T item);
    public Task DeleteByIdAsync(int id);
}