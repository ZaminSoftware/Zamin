namespace Zamin.Utilities.SoftwareParts.Detection.Options;

public class RegistrationOption
{
    public bool Enabled { get; set; } = true;
    public bool IgnoreSSL { get; set; } = false;
    public string BaseAddress { get; set; } = string.Empty;
    public string ServicePath { get; set; } = string.Empty;
}