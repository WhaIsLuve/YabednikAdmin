
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using AdminPanelPro.Services;
using AdminPanelPro.ViewModels;
using AdminPanelPro.Views;

namespace AdminPanelPro;

public partial class App : Application
{
    public static IHost AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddHttpClient<ApiService>(c =>
                {
                    c.BaseAddress = new Uri("http://192.168.31.209:5000/");
                });

                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainViewModel>();

                services.AddTransient<UsersViewModel>();
                services.AddTransient<CriteriaViewModel>();
                services.AddTransient<EntitiesViewModel>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost.StartAsync();

        var window = AppHost.Services.GetRequiredService<MainWindow>();
        window.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost.StopAsync();
        base.OnExit(e);
    }
}
