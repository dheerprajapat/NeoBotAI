using BlazorQrCodeScanner.Maui;
using Microsoft.Extensions.Logging;
using NeoBotAI.Services;
using NeoBotAI.Session;

namespace NeoBotAI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			}).ConfigureMauiQrCodeScanner(); 

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<AIService>();
		builder.Services.AddSingleton<SessionManager>();

		return builder.Build();
	}
}
