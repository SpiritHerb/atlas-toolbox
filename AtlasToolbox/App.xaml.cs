using AtlasToolbox;
using Microsoft.UI.Xaml;
using AtlasToolbox.HostBuilder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AtlasToolbox.Models;
using System.Diagnostics;
using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Runtime.CompilerServices;
using AtlasToolbox.Utils;
using Windows.Media.Protection.PlayReady;
using AtlasToolbox.ViewModels;
using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Views;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AtlasToolbox
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static IHost _host { get; set; }

        public static MainWindow? MainAppWindow { get; private set; }
        public App()
        {
            _host = CreateHostBuilder().Build();
            _host.Start();
            this.InitializeComponent();
        }

        private static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => 
                { 
                    services.AddSingleton<IDialogService, DialogService>(); 
                    services.AddSingleton<GeneralConfigViewModel>();
                    services.AddSingleton<MainWindow>();
                })   
                .AddStores()
                .AddServices()
                .AddViewModels();
        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            var mainWindow = _host?.Services.GetRequiredService<MainWindow>();
            MainAppWindow = mainWindow;
            mainWindow?.Activate();
            mainWindow.DispatcherQueue.TryEnqueue(() => 
            { 
                var dialogService = _host.Services.GetRequiredService<IDialogService>() as DialogService; 
                if (dialogService != null) { dialogService.SetXamlRoot(mainWindow.Content.XamlRoot); 
                } 
            }); 
        }

        private Window m_window;
    }
}
