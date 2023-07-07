namespace Domain.Exceptions;

public class HouseholdException : Exception
{
    public HouseholdException() { }

    public HouseholdException(string message) : base(message)
    {
    }

    public HouseholdException(string message, Exception inner) : base(message, inner)
    {
        
    }
}