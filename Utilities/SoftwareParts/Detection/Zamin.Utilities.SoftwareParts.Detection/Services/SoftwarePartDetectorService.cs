using Microsoft.Extensions.Options;
using Zamin.Utilities.SoftwareParts.Detection.DataModel;
using Zamin.Utilities.SoftwareParts.Detection.Detectors;
using Zamin.Utilities.SoftwareParts.Detection.Options;

namespace Zamin.Services;

public class SoftwarePartDetectorService(SoftwarePartDetector softwarePartDetector,
                                         IOptions<SoftwarePartDetectionOption> softwarePartDetectorOption)
{
    private readonly SoftwarePartDetectionOption _softwarePartDetectorOption = softwarePartDetectorOption.Value;
    private readonly SoftwarePartDetector _softwarePartDetector = softwarePartDetector;

    public SoftwarePart Execute()
    {
        if (string.IsNullOrEmpty(_softwarePartDetectorOption.ApplicationName))
            throw new ArgumentNullException("SoftwareName in SoftwarePartDetectorOption is null");

        var softwareParts = _softwarePartDetector.Detect(_softwarePartDetectorOption.ApplicationName,
                                                         _softwarePartDetectorOption.ModuleName,
                                                         _softwarePartDetectorOption.ServiceName);

        return softwareParts;
    }
}