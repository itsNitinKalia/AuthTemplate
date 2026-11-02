namespace AuthTemplate.Infrastructure.Security;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Issuer { get; set; } = "AuthTemplate";
    public string Audience { get; set; } = "AuthTemplateClient";
    public string Key { get; set; } = "ThisIsADevelopmentKeyForAuthTemplate123!";
    public int ExpiryMinutes { get; set; } = 60;
}
