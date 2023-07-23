using Domain.Entities.Authentication;
using Infrastructure.Authentication.Interfaces;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;

namespace Infrastructure.Authentication.MemberAuthentication;

public class MemberService : IMemberService
{
    private readonly IApiDbContext _dbContext;
    private readonly ILogger<MemberService> _logger;
    private IPasswordHasher _passwordHasher;
    private readonly IGenerateTokenService _generateTokenService;

    public MemberService(IApiDbContext dbContext, ILogger<MemberService> logger, IPasswordHasher passwordHasher, IGenerateTokenService generateTokenService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _passwordHasher = passwordHasher;
        _generateTokenService = generateTokenService;
    }

    public async Task<string> LoginReturnTokenAsync(MemberEntity member)
    {
        // username check should throw try catch this for own errors!
        var getMember = await _dbContext.Members.Where(k => k.Username.ToLower() == member.Username.ToLower()).FirstAsync();
        
        
        // check password
        if (!_passwordHasher.Check(getMember.Password, member.Password))
        {
            throw new Exception("Wrong password");
        }
        
        // we can verify
        var token =  await _generateTokenService.GenerateToken(getMember.HouseholdId, getMember.Permission);

        return token;
    }
    
    
    
    
}