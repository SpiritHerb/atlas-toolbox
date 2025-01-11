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
using AtlasToolbox.Utils;
using Windows.Graphics.Imaging;
using CommunityToolkit.WinUI;
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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public static IHost _host { get; set; }

        public static Window m_window;
        public static Window s_window;
        public static Window c_window;

        public static ContentDialog contentDialog { get; set; }

        public static XamlRoot XamlRoot { get; set; }

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
                c_window = new ControlDialogWindowHelper();
               s_window.Activate();

               InitializeVMAsync();
           }

            //Task.Delay(100).ContinueWith(_ =>
            //{
            //    LogOffComputer();
            //});
        }

        //public static void OnNewTabLoaded(XamlRoot xamlRoot)
        //{
        //    xamlRoot = XamlRoot;
        //}

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

        public static void InitializeContentDialog(string contentDialogType)
        {
            //c_window = new ControlDialogWindowHelper();
            var contentDialogWindow = c_window.Content as ControlDialogView;
            contentDialogWindow.ContentDialogHelper(contentDialogType);
            c_window.Activate();
        }

        //public async static void LogOffComputer()
        //{
        //    if (m_window.Content is FrameworkElement frameworkElement)
        //    {
        //        await frameworkElement.DispatcherQueue.EnqueueAsync(() =>
        //        {
        //            ContentDialog dialog = new ContentDialog()
        //            {
        //                Title = "Do you really wish to delete this profile?",
        //                PrimaryButtonText = "Yes",
        //                CloseButtonText = "Cancel",
        //                DefaultButton = ContentDialogButton.Primary,
        //                XamlRoot = frameworkElement.XamlRoot
        //            };

        //            return dialog.ShowAsync().AsTask();
        //        });
        //    }
        //    else
        //    {
        //        // Handle the case where m_window.Content is not a FrameworkElement
        //        throw new InvalidOperationException("m_window.Content is not a FrameworkElement");
        //    }
        //    //CommandPromptHelper.RunCustomFile("C:\\Windows\\AtlasModules\\Scripts\\logoffPrompt.bat");
        //    //UIElement rootElement = GetXamlRoot();
        //    //try
        //    //{
        //    //    //var test = m_window.Content.GetType;

        //    //    // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
        //    //    //xamlRoot = m_window.Content.XamlRoot;
        //    //    //ContentDialog contentDialog = new ContentDialog();

        //    //    //if (m_window.Content is FrameworkElement framework)
        //    //    //{
        //    //    //    ContentDialog contentDialog = new ContentDialog()
        //    //    //    {
        //    //    //        Title = "Do you really wish to delete this profile?",
        //    //    //        PrimaryButtonText = "Yes",
        //    //    //        CloseButtonText = "Cancel",
        //    //    //        DefaultButton = ContentDialogButton.Primary,
        //    //    //        XamlRoot = framework.XamlRoot
        //    //    //    };
        //    //    //    //var result = await App.contentDialog.ShowAsync();
        //    //    //}

        //    //    contentDialog.XamlRoot = XamlRoot;
        //    //    contentDialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        //    //    contentDialog.Title = "Do you really wish to delete this profile?";
        //    //    contentDialog.PrimaryButtonText = "Yes";
        //    //    contentDialog.CloseButtonText = "Cancel";
        //    //    contentDialog.DefaultButton = ContentDialogButton.Primary;

        //    //    var result = await contentDialog.ShowAsync();
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    logger.Error($"Error on ContentDialog initialization: {ex.Message}");
        //    //}
        //}
    }
}
