using MonkeyFinder.View;
using Microsoft.Extensions.Logging;
using MonkeyFinder.Services;

namespace MonkeyFinder;

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
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<MonkeyService>();
		builder.Services.AddSingleton(Connectivity.Current);
		builder.Services.AddSingleton(Geolocation.Default);
		builder.Services.AddSingleton(Map.Default);
		
		builder.Services.AddSingleton<MonkeysViewModel>();
		builder.Services.AddTransient<MonkeyDetailsViewModel>();
		
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<DetailsPage>();
		
		return builder.Build();
	}
}
