namespace Zamin.Utilities.SoftwareParts.Authorization.Options;

public class SoftwarePartAuthorizationOption
{
    public string ApplicationName { get; set; } = string.Empty;
    public string ModuleName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public LoadAccessListOption FetchAccessList { get; set; } = new();
    public AuthenticationOption Authentication { get; set; } = new();
}

public class AuthenticationOption
{

}

public class LoadAccessListOption
{

}