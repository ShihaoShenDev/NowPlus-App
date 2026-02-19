using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;
using NowPlus.Cross.PageModels;
using NowPlus.Cross.Pages;

namespace NowPlus.Cross
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("SegoeUI-Semibold.ttf", "SegoeSemibold");
                    fonts.AddFont("FluentSystemIcons-Regular.ttf", FluentUI.FontFamily);
                });

#if DEBUG
    		builder.Logging.AddDebug();
    		builder.Services.AddLogging(configure => configure.AddDebug());
#endif

            builder.Services.AddSingleton<ClockPageModel>();
            builder.Services.AddSingleton<StopwatchPageModel>();
            builder.Services.AddSingleton<TimerPageModel>();

            builder.Services.AddSingleton<ClockPage>();
            builder.Services.AddSingleton<StopwatchPage>();
            builder.Services.AddSingleton<TimerPage>();

            return builder.Build();
        }
    }
}
