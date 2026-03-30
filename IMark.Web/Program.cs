using IMark.Shared.Interfaces;
using IMark.Shared.Services;
using IMark.Web;
using IMark.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<TimeCheckService>();

builder.Services.AddSingleton<ITokenStorage, LocalStorageTokenStorage>();
builder.Services.AddTransient<AuthTokenHandler>();
builder.Services.AddSingleton<AuthService>();
builder.Services.AddSingleton<AuthenticationStateProvider, JwtAuthStateProvider>();
builder.Services.AddSingleton<JwtAuthStateProvider>(sp =>
    (JwtAuthStateProvider)sp.GetRequiredService<AuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();

builder.Services.AddSingleton(sp =>
{
    var handler = sp.GetRequiredService<AuthTokenHandler>();
    handler.InnerHandler = new HttpClientHandler();
    return new HttpClient(handler)
    {
        BaseAddress = new Uri("https://localhost:7094")
    };
});

await builder.Build().RunAsync();