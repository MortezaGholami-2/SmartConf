﻿using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

using CommunityToolkit.WinUI.Notifications;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SmartConf.WPF.Activation;
using SmartConf.WPF.Contracts.Activation;
using SmartConf.WPF.Contracts.Services;
using SmartConf.WPF.Contracts.Views;
using SmartConf.WPF.Core.Contracts.Services;
using SmartConf.WPF.Core.Services;
using SmartConf.WPF.Models;
using SmartConf.WPF.Services;
using SmartConf.WPF.ViewModels;
using SmartConf.WPF.Views;

namespace SmartConf.WPF;

// For more information about application lifecycle events see https://docs.microsoft.com/dotnet/framework/wpf/app-development/application-management-overview

// WPF UI elements use language en-US by default.
// If you need to support other cultures make sure you add converters and review dates and numbers in your UI to ensure everything adapts correctly.
// Tracking issue for improving this is https://github.com/dotnet/wpf/issues/1946
public partial class App : Application
{
    private IHost _host;

    public T GetService<T>()
        where T : class
        => _host.Services.GetService(typeof(T)) as T;

    public App()
    {
    }

    private async void OnStartup(object sender, StartupEventArgs e)
    {
        // https://docs.microsoft.com/windows/apps/design/shell/tiles-and-notifications/send-local-toast?tabs=desktop
        ToastNotificationManagerCompat.OnActivated += (toastArgs) =>
        {
            Current.Dispatcher.Invoke(async () =>
            {
                var config = GetService<IConfiguration>();
                config[ToastNotificationActivationHandler.ActivationArguments] = toastArgs.Argument;
                await _host.StartAsync();
            });
        };

        // TODO: Register arguments you want to use on App initialization
        var activationArgs = new Dictionary<string, string>
        {
            { ToastNotificationActivationHandler.ActivationArguments, string.Empty }
        };
        var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        // For more information about .NET generic host see  https://docs.microsoft.com/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0
        _host = Host.CreateDefaultBuilder(e.Args)
                .ConfigureAppConfiguration(c =>
                {
                    c.SetBasePath(appLocation);
                    c.AddInMemoryCollection(activationArgs);
                })
                .ConfigureServices(ConfigureServices)
                .Build();

        if (ToastNotificationManagerCompat.WasCurrentProcessToastActivated())
        {
            // ToastNotificationActivator code will run after this completes and will show a window if necessary.
            return;
        }

        await _host.StartAsync();
    }

    private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        // TODO: Register your services, viewmodels and pages here

        // App Host
        services.AddHostedService<ApplicationHostService>();

        // Activation Handlers
        services.AddSingleton<IActivationHandler, ToastNotificationActivationHandler>();

        // Core Services
        services.AddSingleton<IFileService, FileService>();

        // Services
        services.AddSingleton<IToastNotificationsService, ToastNotificationsService>();
        services.AddSingleton<IWindowManagerService, WindowManagerService>();
        services.AddSingleton<IApplicationInfoService, ApplicationInfoService>();
        services.AddSingleton<ISystemService, SystemService>();
        services.AddSingleton<IPersistAndRestoreService, PersistAndRestoreService>();
        services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
        services.AddSingleton<ISampleDataService, SampleDataService>();
        services.AddSingleton<IPageService, PageService>();
        services.AddSingleton<INavigationService, NavigationService>();

        // Views and ViewModels
        services.AddTransient<IShellWindow, ShellWindow>();
        services.AddTransient<ShellViewModel>();

        services.AddTransient<MainViewModel>();
        services.AddTransient<MainPage>();

        services.AddTransient<ConfigurationsViewModel>();
        services.AddTransient<ConfigurationsPage>();

        services.AddTransient<DataGridViewModel>();
        services.AddTransient<DataGridPage>();

        services.AddTransient<ContentGridViewModel>();
        services.AddTransient<ContentGridPage>();

        services.AddTransient<ContentGridDetailViewModel>();
        services.AddTransient<ContentGridDetailPage>();

        services.AddTransient<ListDetailsViewModel>();
        services.AddTransient<ListDetailsPage>();

        services.AddTransient<SettingsViewModel>();
        services.AddTransient<SettingsPage>();

        services.AddTransient<IShellDialogWindow, ShellDialogWindow>();
        services.AddTransient<ShellDialogViewModel>();

        // Configuration
        services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
        services.AddSingleton<IRightPaneService, RightPaneService>();
    }

    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();
        _host = null;
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // TODO: Please log and handle the exception as appropriate to your scenario
        // For more info see https://docs.microsoft.com/dotnet/api/system.windows.application.dispatcherunhandledexception?view=netcore-3.0
    }
}
