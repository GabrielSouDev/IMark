using IMark.Shared.Interfaces;

namespace IMark.App.Services;

public class SecureTokenStorage : ITokenStorage
{
    public async Task SaveTokenAsync(string token) =>
        await SecureStorage.SetAsync("jwt_token", token);

    public async Task<string?> GetTokenAsync() =>
        await SecureStorage.GetAsync("jwt_token");

    public async Task RemoveTokenAsync() =>
        SecureStorage.Remove("jwt_token");
}