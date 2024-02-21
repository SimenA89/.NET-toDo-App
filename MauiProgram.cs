using Microsoft.Extensions.Logging;
using ToDo.ViewModels;


namespace ToDo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            // Register services and view models
            builder.UseMauiApp<App>();

            // Register MainPage and wrap it in a NavigationPage
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<NavigationPage>(s => new NavigationPage(s.GetRequiredService<MainPage>()));
            // Register view models and services
            builder.Services.AddSingleton<TasksViewModel>();
            builder.Services.AddSingleton<TaskDataService>();

            builder.Services.AddSingleton<App>();
            builder.ConfigureFonts(fonts =>
               {
                   fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                   fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
               });
            // ... other configurations ...
            return builder.Build();
        }
    }
}
