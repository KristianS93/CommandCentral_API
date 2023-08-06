namespace Application.Interfaces;

public interface IBaseService<TService>
{
    TService GetByItem(TService? item);
    TService Create(TService? item);
    TService Delete(TService? item);
    TService Update(TService? item);
}