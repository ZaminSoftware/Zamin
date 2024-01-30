using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zamin.Utilities.SoftwareParts.Detection.Options;

namespace Zamin.Utilities.SoftwareParts.Detection.Services;

public class SoftwarePartAuthenticatorService(HttpClient httpClient,
                                              IOptions<SoftwarePartDetectionOption> softwarePartDetectorOption,
                                              ILoggerFactory loggerFactory)
{

    private readonly HttpClient _httpClient = httpClient;
    private readonly SoftwarePartDetectionOption _option = softwarePartDetectorOption.Value;
    private readonly ILogger<SoftwarePartAuthenticatorService> _logger = loggerFactory.CreateLogger<SoftwarePartAuthenticatorService>();

    public async Task<TokenResponse> ExecuteAsync()
    {
        var discoveryDocumentResponse = await _httpClient.GetDiscoveryDocumentAsync(_option.Authentication.Authority);

        if (discoveryDocumentResponse.IsError)
        {
            _logger.LogError(message: discoveryDocumentResponse.Error);
            throw new Exception(discoveryDocumentResponse.Error);
        }

        if (discoveryDocumentResponse is null)
        {
            _logger.LogError("");
            throw new ArgumentNullException(nameof(DiscoveryDocumentResponse));
        }

        var tokenRequest = new ClientCredentialsTokenRequest
        {
            Address = discoveryDocumentResponse.TokenEndpoint,
            ClientId = _option.Authentication.ClientId,
            ClientSecret = _option.Authentication.ClientSecret,
            Scope = string.Join(',', _option.Authentication.Scopes),
        };

        var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(tokenRequest);

        if (tokenResponse is null)
        {
            _logger.LogError("");
            throw new ArgumentNullException(nameof(TokenResponse));
        }

        if (tokenResponse.IsError)
        {
            _logger.LogError(message: tokenResponse.Error);
            throw new Exception(tokenResponse.Error);
        }

        return tokenResponse;
    }
}