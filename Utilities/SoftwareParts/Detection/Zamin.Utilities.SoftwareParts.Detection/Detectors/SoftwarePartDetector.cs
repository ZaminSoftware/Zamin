using Zamin.Utilities.SoftwareParts.Detection.DataModel;

namespace Zamin.Utilities.SoftwareParts.Detection.Detectors;

public class SoftwarePartDetector(ControllersAndActionDetector controllersAndActionDetector)
{
    private readonly List<SoftwarePartController> _controllers = controllersAndActionDetector.Detect();

    public SoftwarePart Detect(string softwareName)
    {
        if (string.IsNullOrWhiteSpace(softwareName))
            throw new ArgumentNullException(nameof(softwareName));

        var application = new SoftwarePart
        {
            Name = softwareName,
            SoftwarePartType = SoftwarePartType.Software,
            Children = _controllers.Select(controller => (SoftwarePart)controller).ToList(),
        };

        return application;
    }

    public SoftwarePart Detect(string softwareName, string moduleName)
    {
        if (string.IsNullOrEmpty(moduleName))
            return Detect(softwareName);

        var application = new SoftwarePart
        {
            Name = softwareName,
            SoftwarePartType = SoftwarePartType.Software,
            Children =
            [
                new()
                {
                    Name = moduleName,
                    SoftwarePartType = SoftwarePartType.Module,
                    Children = _controllers.Select(controller => (SoftwarePart)controller).ToList(),
                }
            ]
        };

        return application;
    }

    public SoftwarePart Detect(string softwareName, string moduleName, string serviceName)
    {
        if (string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(moduleName))
            return Detect(softwareName, moduleName);

        var application = new SoftwarePart
        {
            Name = softwareName,
            SoftwarePartType = SoftwarePartType.Software,
            Children =
            [
                new()
                {
                    Name = moduleName,
                    SoftwarePartType = SoftwarePartType.Module,
                    Children =
                    [
                        new()
                        {
                            Name = serviceName,
                            SoftwarePartType = SoftwarePartType.Service,
                            Children = _controllers.Select(controller => (SoftwarePart)controller).ToList(),
                        }
                    ]
                }
            ]
        };

        return application;
    }
}