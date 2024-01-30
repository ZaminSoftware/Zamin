using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Zamin.Utilities.SoftwareParts.Detection.DataModel;

namespace Zamin.Utilities.SoftwareParts.Detection.Detectors;

public class ControllersAndActionDetector(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
{
    private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;

    public List<SoftwarePartController> Detect()
    {
        var result = new List<SoftwarePartController>();

        var controllerActionDescriptors = _actionDescriptorCollectionProvider.ActionDescriptors.Items.OfType<ControllerActionDescriptor>().ToList();

        foreach (var item in controllerActionDescriptors)
        {
            result.Add(new SoftwarePartController
            {
                Name = item.ControllerName,
                ApplicationPartType = SoftwarePartType.Controller,
            });
        }

        result = result.DistinctBy(c => c.Name).GroupJoin(controllerActionDescriptors, c => c.Name, a => a.ControllerName, (c, a) => new SoftwarePartController
        {
            Name = c.Name,
            ApplicationPartType = SoftwarePartType.Controller,
            Actions = a.Select(b => new SoftwarePartAction
            {
                Name = b.ActionName,
                ApplicationPartType = SoftwarePartType.Action
            }).DistinctBy(c => c.Name).ToList()
        }).ToList();

        return result;
    }
}