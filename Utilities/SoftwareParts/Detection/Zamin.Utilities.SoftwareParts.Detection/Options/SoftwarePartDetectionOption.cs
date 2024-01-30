namespace Zamin.Utilities.SoftwareParts.Detection.Options;

public class SoftwarePartDetectionOption
{
    public string ApplicationName { get; set; } = string.Empty;
    public string ModuleName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public RegistrationOption Registration { get; set; } = new();
    public AuthenticationOption Authentication { get; set; } = new();
}