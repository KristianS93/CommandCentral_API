using Domain.Entities;

namespace Infrastructure.Authentication.Interfaces;

public interface IJwtProvider
{
    string Generate(HouseholdEntity household);
}