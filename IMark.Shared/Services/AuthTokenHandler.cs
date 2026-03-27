using IMark.Shared.Interfaces;
using System.Net.Http.Headers;

namespace IMark.Shared.Services;

public class AuthTokenHandler : DelegatingHandler
{
    private readonly ITokenStorage _tokenStorage;

    public AuthTokenHandler(ITokenStorage tokenStorage) => _tokenStorage = tokenStorage;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
    {
        var token = await _tokenStorage.GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, ct);
    }
}