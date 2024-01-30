namespace Zamin.Utilities.SoftwareParts.Detection.Options;

public class AuthenticationOption
{
    public bool Enabled { get; set; } = false;
    public bool IgnoreSSL { get; set; } = false;
    public string Authority { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string[] Scopes { get; set; } = [];
}