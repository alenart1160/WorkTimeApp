using Microsoft.Extensions.Logging;
using WorkTime.Abstractions;
using WorkTimeApp.Services;
using WorkTimeApp.Shared.Model;
using WorkTimeApp.Shared.Services;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;

namespace WorkTimeApp;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        }).UseMauiCommunityToolkit();
        // Add device-specific services used by the WorkTimeApp.Shared project
        builder.Services.AddSingleton<IFormFactor, FormFactor>();
        builder.Services.AddSingleton<ApiService>();
        builder.Services.AddSingleton<IPlatformInfo, PlatformService>();
        builder.Services.AddSingleton<IExportToExcel, SaveExcelFile>();
        builder.Services.AddSingleton(new HttpClient());
        builder.Services.AddScoped<UserModel>();
        builder.Services.AddSingleton<UserModel>();
        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}