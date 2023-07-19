namespace Infrastructure.Authentication;

public class JwtOptions
{
    public JwtOptions()
    {
    }

    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
}