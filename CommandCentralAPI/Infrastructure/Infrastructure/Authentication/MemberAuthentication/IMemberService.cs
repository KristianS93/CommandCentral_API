using Domain.Entities.Authentication;

namespace Infrastructure.Authentication.MemberAuthentication;

public interface IMemberService
{
    Task<string> LoginReturnTokenAsync(MemberEntity member);
}