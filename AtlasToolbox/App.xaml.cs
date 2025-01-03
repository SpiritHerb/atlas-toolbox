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
using NLog;
using NLog.Config;
using NLog.Targets;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AtlasToolbox
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public static IHost _host { get; set; }

        public static Window m_window;
        public static Window s_window;


        private static Mutex _mutex = new Mutex(true, "{AtlasToolbox}");

        public App()
        {
            ConfigureNLog();
            logger.Info("App Started");
            _host = CreateHostBuilder().Build();
            logger.Info("Building host");
            _host.Start();
            logger.Info("Starting host");
            this.InitializeComponent();
            logger.Info("Finished initializing components");
            this.UnhandledException += OnAppUnhandledException;
        }

        private static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .AddStores()
                .AddServices()
                .AddViewModels();

        private void ConfigureNLog()
        {
            //This is weird and there's probably a better way to do it, for now it works but is to change
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            int hour = DateTime.Now.Hour;
            int minutes = DateTime.Now.Minute;
            int seconds = DateTime.Now.Second;

            string name = $"logs/toolbox-log-{year}_{month}_{day}_{hour}_{minutes}_{seconds}.log";
            var config = new LoggingConfiguration();
            var logfile = new FileTarget("logfile")
            {
                FileName = name,
                Layout = "${longdate} ${level}: ${message} ${exception}"
            };
            config.AddTarget(logfile); config.AddRuleForAllLevels(logfile);
            LogManager.Configuration = config;
        }

        private void OnAppUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            logger.Error(e.Exception, "Unhandled exception occurred");
            //e.Handled = true; 
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
           Task.Run(() => StartNamedPipeServer());

           if (!_mutex.WaitOne(TimeSpan.Zero, true))
           {
               CheckForExistingInstance();
               Environment.Exit(0);
               return;
           }

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
                logger.Info("Loading without args");
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
            logger.Info("Loading configuration services");
            await Task.Run(() => _host.Services.GetRequiredService<GeneralConfigViewModel>());
            logger.Info("Configuration services loaded");
            m_window = new MainWindow();
            m_window.Activate();
            s_window.Close();
        }
    }
}
