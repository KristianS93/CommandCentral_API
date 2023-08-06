namespace Domain.Exceptions.GroceryList;

public class GroceryListDuplicateException : Exception
{
    public GroceryListDuplicateException(string message) : base(message)
    {
    }

    public GroceryListDuplicateException(string message, Exception inner) : base(message, inner)
    {
        
    }
}