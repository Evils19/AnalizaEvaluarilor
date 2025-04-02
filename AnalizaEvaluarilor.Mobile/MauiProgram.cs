using AnalizaEvaluarilor.Mobile.Service;
using Microsoft.Extensions.Logging;

namespace AnalizaEvaluarilor.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddSingleton<IAppService, AppService>();
        builder.Services.AddScoped<CameraService>();
        
        builder.Services.AddAuthorizationCore();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

