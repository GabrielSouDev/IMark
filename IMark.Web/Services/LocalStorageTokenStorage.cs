using IMark.Shared.Interfaces;
using Microsoft.JSInterop;

namespace IMark.Web.Services;

public class LocalStorageTokenStorage : ITokenStorage
{
    private readonly IJSRuntime _js;
    public LocalStorageTokenStorage(IJSRuntime js) => _js = js;

    public async Task SaveTokenAsync(string token) =>
        await _js.InvokeVoidAsync("localStorage.setItem", "jwt_token", token);

    public async Task<string?> GetTokenAsync() =>
        await _js.InvokeAsync<string?>("localStorage.getItem", "jwt_token");

    public async Task RemoveTokenAsync() =>
        await _js.InvokeVoidAsync("localStorage.removeItem", "jwt_token");
}