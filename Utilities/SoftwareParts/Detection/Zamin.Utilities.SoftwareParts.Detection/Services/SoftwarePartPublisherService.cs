using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using Zamin.Extensions.Serializers.Abstractions;
using Zamin.Utilities.SoftwareParts.Detection.Options;
using Zamin.Utilities.SoftwareParts.Registration.DataModel;

namespace Zamin.Utilities.SoftwareParts.Detection.Services;

public class SoftwarePartPublisherService(HttpClient httpClient,
                                          SoftwarePartDetectorService detectorService,
                                          SoftwarePartAuthenticatorService authenticatorService,
                                          IJsonSerializer jsonSerializer,
                                          ILoggerFactory loggerFactory,
                                          IOptions<SoftwarePartDetectionOption> softwarePartDetectorOption)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly SoftwarePartDetectorService _detectorService = detectorService;
    private readonly SoftwarePartAuthenticatorService _authenticatorService = authenticatorService;
    private readonly IJsonSerializer _jsonSerializer = jsonSerializer;
    private readonly SoftwarePartDetectionOption _option = softwarePartDetectorOption.Value;
    private readonly ILogger<SoftwarePartPublisherService> _logger = loggerFactory.CreateLogger<SoftwarePartPublisherService>();

    public async Task ExecuteAsync()
    {
        if (_option.Authentication.Enabled)
        {
            var token = await _authenticatorService.ExecuteAsync();

            _httpClient.SetBearerToken(token.AccessToken);
        }

        var softwarePart = _detectorService.Execute();

        StringContent content = new(_jsonSerializer.Serialize(softwarePart), Encoding.UTF8, "application/json");

        var publishResponse = await _httpClient.PostAsync(_option.ServiceName, content);

        if (publishResponse.IsSuccessStatusCode)
        {
            _logger.LogInformation("");
        }
        else
        {
            _logger.LogError("");
        }
    }
}