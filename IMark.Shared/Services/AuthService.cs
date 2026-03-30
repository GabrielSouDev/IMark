using IMark.Shared.Interfaces;
using IMark.Shared.Models.Requests;
using System.Net.Http.Json;

namespace IMark.Shared.Services;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly ITokenStorage _tokenStorage;

    public AuthService(HttpClient http, ITokenStorage tokenStorage)
    {
        _http = http;
        _tokenStorage = tokenStorage;
    }

    public async Task<bool> LoginAsync(LoginRequest loginRequest)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", loginRequest);
        if (!response.IsSuccessStatusCode) return false;

        var result = await response.Content.ReadFromJsonAsync<LoginResult>();
        if (result?.Token == null) return false;

        await _tokenStorage.SaveTokenAsync(result.Token);
        return true;
    }

    public async Task LogoutAsync() => await _tokenStorage.RemoveTokenAsync();
}

public class LoginResult
{
    public string Token { get; set; } = string.Empty;
}