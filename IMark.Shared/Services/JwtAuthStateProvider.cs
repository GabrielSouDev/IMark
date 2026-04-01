using IMark.Shared.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace IMark.Shared.Services;

public class JwtAuthStateProvider : AuthenticationStateProvider
{
    private readonly ITokenStorage _tokenStorage;

    public JwtAuthStateProvider(ITokenStorage tokenStorage)
        => _tokenStorage = tokenStorage;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _tokenStorage.GetTokenAsync();

        if (string.IsNullOrEmpty(token) || IsTokenExpired(token))
        {
            if (!string.IsNullOrEmpty(token))
                await _tokenStorage.RemoveTokenAsync();

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var claims = ParseClaimsFromJwt(token);
        var identity = new ClaimsIdentity(claims, "jwt");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public async Task NotifyLoginAsync()
    {
        var state = await GetAuthenticationStateAsync();
        NotifyAuthenticationStateChanged(Task.FromResult(state));
    }

    public async Task NotifyLogoutAsync()
    {
        await _tokenStorage.RemoveTokenAsync();
        var empty = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        NotifyAuthenticationStateChanged(Task.FromResult(empty));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var padded = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
        var json = Encoding.UTF8.GetString(Convert.FromBase64String(padded));
        return JsonSerializer.Deserialize<Dictionary<string, object>>(json)!
            .Select(kv => new Claim(kv.Key, kv.Value.ToString()!));
    }

    private static bool IsTokenExpired(string token)
    {
        try
        {
            var payload = token.Split('.')[1];
            var padded = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
            var json = Encoding.UTF8.GetString(Convert.FromBase64String(padded));
            var claims = JsonSerializer.Deserialize<Dictionary<string, object>>(json)!;

            if (claims.TryGetValue("exp", out var exp))
            {
                var expSeconds = long.Parse(exp.ToString()!);
                var expDate = DateTimeOffset.FromUnixTimeSeconds(expSeconds).UtcDateTime;
                return expDate < DateTime.UtcNow;
            }

            return true; 
        }
        catch
        {
            return true;
        }
    }
}