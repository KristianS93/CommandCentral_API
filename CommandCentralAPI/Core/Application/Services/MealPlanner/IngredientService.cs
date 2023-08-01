using Application.Interfaces.MealPlanner;
using Domain.Entities.MealPlanner;

namespace Application.Services.MealPlanner;

public class IngredientService : IIngredientService
{
    private const string MissingNameOrAmount = "Missing name or amount!";
    
    public IngredientEntity GetByItem(IngredientEntity? item)
    {
        ArgumentNullException.ThrowIfNull(item);
        return item;
    }

    public IngredientEntity Create(IngredientEntity? item)
    {
        
        // Validation
        Console.WriteLine("In create service");
        if (String.IsNullOrEmpty(item.Name) || String.IsNullOrEmpty(item.Amount))
        {
            throw new ArgumentException(MissingNameOrAmount);
        }
        
        var hasLetters = item.Name.Any(Char.IsLetter);
        if (!hasLetters)
        {
            throw new ArgumentException("Name doesnt have any letters");
        }

        // consider utc time.
        var createdAt = DateTime.Now;
        item.CreatedAt = createdAt;
        item.LastModified = createdAt;

        return item;
    }

    public IngredientEntity Delete(IngredientEntity? item)
    {
        ArgumentNullException.ThrowIfNull(item);
        return item;
    }

    public IngredientEntity Update(IngredientEntity? item)
    {
        if (String.IsNullOrEmpty(item.Name) || String.IsNullOrEmpty(item.Amount))
        {
            throw new ArgumentException(MissingNameOrAmount);
        }
        var hasLetters = item.Name.Any(Char.IsLetter);
        if (!hasLetters)
        {
            throw new ArgumentException("Name doesnt have any letters");
        }
        
        // update last modified
        item.LastModified = DateTime.Now;
        return item;
    }
}