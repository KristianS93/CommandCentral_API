using Application.Interfaces.MealPlanner;
using Domain.Entities.MealPlanner;

namespace Application.Services.MealPlanner;

public class MealService : IMealService
{
    public MealEntity GetByItem(MealEntity? item)
    {
        ArgumentNullException.ThrowIfNull(item);
        return item;
    }

    public MealEntity Create(MealEntity? item)
    {
        ArgumentNullException.ThrowIfNull(item);
        if (String.IsNullOrEmpty(item.Name) || item.Name.Any(Char.IsLetter))
        {
            throw new ArgumentException("Missing name, or wrong format!");
        }

        var createdAt = DateTime.Now;
        item.CreatedAt = createdAt;
        item.LastModified = createdAt;

        return item;
    }

    public MealEntity Delete(MealEntity? item)
    {
        ArgumentNullException.ThrowIfNull(item);
        return item;
    }

    public MealEntity Update(MealEntity? item)
    {
        ArgumentNullException.ThrowIfNull(item);
        if (String.IsNullOrEmpty(item.Name) || item.Name.Any(Char.IsLetter))
        {
            throw new ArgumentException("Missing name, or wrong format!");
        }
        
        // update
        item.LastModified = DateTime.Now;
        return item;
    }
}