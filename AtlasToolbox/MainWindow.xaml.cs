using System;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using WinUIEx;
using Microsoft.UI.Xaml.Media.Animation;
using System.Runtime.InteropServices;
using AtlasToolbox.Utils;
using CommunityToolkit.WinUI;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using AtlasToolbox.Views;

namespace AtlasToolbox
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            //Window parameters
            WindowManager.Get(this).Width = 1250;
            WindowManager.Get(this).Height = 850;
            WindowManager.Get(this).MinHeight = 850;
            WindowManager.Get(this).MinWidth = 1250;
            CenterWindowOnScreen();
            ExtendsContentIntoTitleBar = true;

            NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems.OfType<NavigationViewItem>().First();
            ContentFrame.Navigate(
                       typeof(Views.HomePage),
                       null,
                       new Microsoft.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo()
                       );
            SetTitleBar(AppTitleBar);


            if (RegistryHelper.IsMatch("HKLM\\SOFTWARE\\AtlasOS\\Toolbox", "OnStartup", 1)) this.Closed += AppBehaviorHelper.HideApp;
            else this.Closed += AppBehaviorHelper.CloseApp;
        }
        
        /// <summary>
        /// Gets the window Xaml root for ContentDialogs
        /// </summary>
        /// <returns></returns>
        public XamlRoot GetXamlRoot()
        {
            return this.Content.XamlRoot;
        }

        public void GoToSoftwarePage()
        {
            App.CurrentCategory = "Software";
            ContentFrame.Navigate(
                   new SoftwarePage().GetType(),
                   null,
                   new DrillInNavigationTransitionInfo()
                   );
            App.XamlRoot = this.Content.XamlRoot;
            NavigationViewControl.Header = "Software downloading";
        }
        
        /// <summary>
        /// navigates to the correct page when a navigation item is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void NavigationViewControl_ItemInvoked(NavigationView sender,
                      NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                App.CurrentCategory = "SettingsItem";
                ContentFrame.Navigate(typeof(Views.SettingsPage), null, new DrillInNavigationTransitionInfo());
                return;
            }
            else if (args.InvokedItemContainer.Tag.ToString() == "AtlasToolbox.Views.SoftwarePage")
            {
                App.CurrentCategory = args.InvokedItemContainer.Tag.ToString();
                ContentFrame.Navigate(
                       new SoftwarePage().GetType(),
                       null,
                       new DrillInNavigationTransitionInfo()
                       );
            }
            else if (args.InvokedItemContainer.Tag.ToString() != "AtlasToolbox.Views.HomePage")
            {
                App.CurrentCategory = args.InvokedItemContainer.Tag.ToString();
                ContentFrame.Navigate(
                       new ConfigPage().GetType(),
                       null,
                       new DrillInNavigationTransitionInfo());
            }
            else if (args.InvokedItemContainer.Tag.ToString() == "AtlasToolbox.Views.HomePage")
            {
                App.CurrentCategory = args.InvokedItemContainer.Tag.ToString();
                Type newPage = Type.GetType(args.InvokedItemContainer.Tag.ToString());
                ContentFrame.Navigate(
                       newPage,
                       null,
                       new DrillInNavigationTransitionInfo());
            }
            App.XamlRoot = this.Content.XamlRoot;
        }

        private void NavigationViewControl_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack) ContentFrame.GoBack();
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            NavigationViewControl.IsBackEnabled = ContentFrame.CanGoBack;
            NavigationViewControl.Header = null;
            if (App.CurrentCategory != null && App.CurrentCategory != "SettingsItem")
            {
                NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(App.CurrentCategory));
            }
            else
            {
                NavigationViewControl.SelectedItem = (NavigationViewItem)NavigationViewControl.SettingsItem;
            }
            if (ContentFrame.SourcePageType == typeof(Views.SettingsPage)) NavigationViewControl.SelectedItem = (NavigationViewItem)NavigationViewControl.SettingsItem;
        }

        /// <summary>
        /// Creates a ContentDialog with the required type
        /// </summary>
        /// <param name="type">type of content dialog</param>
        /// <exception cref="Exception"></exception>
        public async void ContentDialogContoller(string type)
        {
            string title = "", desc = "", primBtnTxt = "";
            ICommand command = null;

            switch (type)
            {
                case "restart":
                    title = "Restart your PC to apply.";
                    desc = "To apply these changes, please restart your PC.";
                    primBtnTxt = "Restart now";
                    command = new RelayCommand(ComputerStateHelper.RestartComputer);
                    break;
                case "logoff":
                    title = "Relog to apply";
                    desc = "To apply these changes, please relog.";
                    primBtnTxt = "Log off";
                    command = new RelayCommand(ComputerStateHelper.LogOffComputer);
                    break;
                default:
                    throw new Exception("ContentDialog type was not set or does not match any possible type");
            }
            await DispatcherQueue.EnqueueAsync(() =>
            {
                ContentDialog dialog = new ContentDialog();

                // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
                dialog.XamlRoot = App.XamlRoot;
                dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
                dialog.Title = title;
                dialog.Content = desc;
                dialog.PrimaryButtonText = primBtnTxt;
                dialog.CloseButtonText = "Later";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.PrimaryButtonCommand = command;

                try
                {
                    var result = dialog.ShowAsync();
                }
                catch
                { App.logger.Error("Program tried to open more than one ContentDialog"); }
            });
        }
        private void CenterWindowOnScreen()
        {
            int screenWidth = GetSystemMetrics(SM_CXSCREEN);
            int screenHeight = GetSystemMetrics(SM_CYSCREEN);

            double centerX = (screenWidth - this.Bounds.Width) / 2;
            double centerY = (screenHeight - this.Bounds.Height) / 2;

            this.MoveAndResize(centerX, centerY, this.Bounds.Width, this.Bounds.Height);
        }


        private void MoveAndResize(double x, double y, double width, double height)
        {
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            SetWindowPos(hwnd, IntPtr.Zero, (int)x, (int)y, (int)width, (int)height, SWP_NOZORDER | SWP_NOACTIVATE);
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOACTIVATE = 0x0010;

        int timesClicked;
        private void AtlasButton_Click(object sender, RoutedEventArgs e)
        {
            if (timesClicked == 10)
            {
                App.f_window = new FWindow();
                App.f_window.Activate();
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
            }
        }
    }
}
