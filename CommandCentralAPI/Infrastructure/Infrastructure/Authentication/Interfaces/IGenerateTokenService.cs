namespace Infrastructure.Authentication.Interfaces;

public interface IGenerateTokenService
{
    Task<string> GenerateToken(int householdId);
}