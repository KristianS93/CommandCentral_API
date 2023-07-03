namespace Infrastructure.Interfaces;

public interface IBaseCRUD<T>
{
    public Task<List<T>> GetAllAsync();
    public Task<T> GetByIdAsync(int id);
    public Task<T> CreateAsync(T item);
    public Task UpdateAsync(T item);
    public Task DeleteAsync(T item);
}