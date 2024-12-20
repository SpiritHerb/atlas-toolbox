using Microsoft.UI.Xaml;
using AtlasToolbox.HostBuilder;
using Microsoft.Extensions.Hosting;
using AtlasToolbox.Views;
using AtlasToolbox.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using WinUIEx;
using System;
using AtlasToolbox.Controls;
using CommunityToolkit.WinUI;

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
        private bool _isLoading;

        public App()
        {
            _host = CreateHostBuilder().Build();
            _host.Start();
            this.InitializeComponent();
        }

        private static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .AddStores()
                .AddServices()
                .AddViewModels();
        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            s_window = new LoadingWindow();
            s_window.Activate();

            InitializeVMAsync();
        }

        //private async void LaunchTask()
        //{
        //    await InitializeVMAsync();
        //}

        private async void InitializeVMAsync()
        {
            await Task.Run(() => _host.Services.GetRequiredService<GeneralConfigViewModel>());
            m_window = new MainWindow();
            m_window.Activate();
            s_window.Close();
        }

        public static Window m_window;
        public static Window s_window;
    }
}
