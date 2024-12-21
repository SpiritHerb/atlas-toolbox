using Microsoft.UI.Xaml;
using AtlasToolbox.HostBuilder;
using Microsoft.Extensions.Hosting;
using AtlasToolbox.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System;
using System.IO.Pipes;
using System.IO;
using Windows.UI.Core;
using WinUIEx;
using Windows.ApplicationModel.Core;
using System.Threading;

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

        public static Window m_window;
        public static Window s_window;

        private static Mutex _mutex = new Mutex(true, "{AtlasToolbox}");

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

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
           Task.Run(() => StartNamedPipeServer());

            if (!_mutex.WaitOne(TimeSpan.Zero, true))
            {
                CheckForExistingInstance();
                Environment.Exit(0);
                return;
            }

            m_window = new MainWindow();
            string[] arguments = Environment.GetCommandLineArgs();
            bool wasRanWithArgs = false;
            foreach (var arg in arguments)
            {
                if (arg.StartsWith("-"))
                {
                    switch (arg)
                    {
                        case "-silent":
                            InitializeVMAsyncSilent();
                            wasRanWithArgs = true;
                            break;
                        case "-toforeground":
                            m_window.Show();
                            wasRanWithArgs = true;
                            break;
                        case "-runEnabled":
                            break;
                        case "-runDefaults":
                            break;
                    }
                }
            }

            if (!wasRanWithArgs)
                {
                    s_window = new LoadingWindow();
                    s_window.Activate();

                    InitializeVMAsync();
                }
        }

        private void CheckForExistingInstance()
        {
            try
            {
                using (var client = new NamedPipeClientStream(".", "pipe", PipeDirection.Out))
                {
                    client.Connect(1000);
                    using (var writer = new StreamWriter(client))
                    {
                        writer.WriteLine("-toforeground");
                        writer.Flush();
                    }
                }
                Environment.Exit(0);
            }
            catch (Exception ex)
            { System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}"); }
        }

        private void StartNamedPipeServer()
        {
            while (true)
            {
                using (var server = new NamedPipeServerStream("pipe", PipeDirection.In))
                {
                    server.WaitForConnection();
                    using (var reader = new StreamReader(server))
                    {
                        string command = reader.ReadLine();
                        if (command == "-toforeground")
                        {
                            m_window.DispatcherQueue.TryEnqueue(() =>
                            {
                                m_window.Activate();
                            });
                        }
                    }
                }
            }
        }
        

        private void InitializeVMAsyncSilent()
        {
            _host.Services.GetRequiredService<GeneralConfigViewModel>();
        }

        private async void InitializeVMAsync()
        {
            await Task.Run(() => _host.Services.GetRequiredService<GeneralConfigViewModel>());
            m_window = new MainWindow();
            m_window.Activate();
            s_window.Close();
        }
    }
}
