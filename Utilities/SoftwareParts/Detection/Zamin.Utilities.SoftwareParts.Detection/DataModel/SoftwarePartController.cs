namespace Zamin.Utilities.SoftwareParts.Detection.DataModel;

public class SoftwarePartController
{
    public string Name { get; set; } = string.Empty;
    public SoftwarePartType ApplicationPartType { get; set; }
    public List<SoftwarePartAction> Actions { get; set; } = [];

    public static explicit operator SoftwarePart(SoftwarePartController entity) => new()
    {
        Name = entity.Name,
        SoftwarePartType = entity.ApplicationPartType,
        Children = entity.Actions.Select(action => (SoftwarePart)action).ToList()
    };
}

public class SoftwarePartAction
{
    public string Name { get; set; } = string.Empty;
    public SoftwarePartType ApplicationPartType { get; set; }

    public static explicit operator SoftwarePart(SoftwarePartAction entity) => new()
    {
        Name = entity.Name,
        SoftwarePartType = entity.ApplicationPartType
    };
}