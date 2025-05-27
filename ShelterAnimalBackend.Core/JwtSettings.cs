namespace ShelterAnimalBackend.Core;

public class JwtSettings
{
   

    public string SecurityKey { get; set; } = null!;
    public string ValidIssuer { get; set; } = null!;
    public string ValidAudience { get; set; } = null!;
    public int ExpiryInMinutes { get; set; }
}