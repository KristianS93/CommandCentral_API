using Domain.Models.ErrorResponses;

namespace Domain.Exceptions;

public class HouseholdDoesNotExistException : Exception
{
    public HouseholdDoesNotExistException()
    {
        
    }
    public HouseholdDoesNotExistException(string message) : base(message)
    {
    }
    
    public HouseholdDoesNotExistException(string message, Exception inner) : base(message, inner)
    {
        
    }
}