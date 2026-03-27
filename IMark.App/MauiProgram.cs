using IMark.App.Services;
using IMark.Shared.Interfaces;
using IMark.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace IMark.App
{
    public static class MauiProgram
    {
        private static string GetBaseAddress()
        {
            #if ANDROID
                        return "https://10.0.2.2:7094";
            #elif IOS || MACCATALYST
                        return "https://localhost:7094";
            #else
                        return "https://localhost:7094";
            #endif
        }

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddSingleton<ITokenStorage, SecureTokenStorage>();
            builder.Services.AddSingleton<AuthTokenHandler>();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<AuthenticationStateProvider, JwtAuthStateProvider>();
            builder.Services.AddSingleton<JwtAuthStateProvider>(sp =>
                (JwtAuthStateProvider)sp.GetRequiredService<AuthenticationStateProvider>());

            builder.Services.AddAuthorizationCore();

            builder.Services.AddSingleton(sp =>
            {
                var handler = sp.GetRequiredService<AuthTokenHandler>();

                #if DEBUG && ANDROID
                    handler.InnerHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback =
                            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };
                #else
                        handler.InnerHandler = new HttpClientHandler();
                #endif

                return new HttpClient(handler)
                {
                    BaseAddress = new Uri(GetBaseAddress())
                };
            });

            #if DEBUG
                        builder.Services.AddBlazorWebViewDeveloperTools();
                        builder.Logging.AddDebug();
            #endif

            return builder.Build();
        }
    }
}