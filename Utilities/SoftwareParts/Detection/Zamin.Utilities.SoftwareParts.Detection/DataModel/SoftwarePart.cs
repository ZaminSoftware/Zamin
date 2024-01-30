namespace Zamin.Utilities.SoftwareParts.Detection.DataModel;

public class SoftwarePart
{
    public string Name { get; set; } = string.Empty;
    public SoftwarePartType SoftwarePartType { get; set; }
    public List<SoftwarePart> Children { get; set; } = [];
}