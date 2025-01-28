using System;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using WinUIEx;
using Microsoft.UI.Xaml.Media.Animation;
using System.Runtime.InteropServices;
using AtlasToolbox.Utils;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
using Windows.UI.Core;
using CommunityToolkit.WinUI;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media.Devices;
using NLog;
using System.Windows.Input;
using NLog.LayoutRenderers.Wrappers;
using CommunityToolkit.Mvvm.Input;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AtlasToolbox
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            //Window parameters
            WindowManager.Get(this).IsMaximizable = false;
            WindowManager.Get(this).IsResizable = false;
            WindowManager.Get(this).Width = 1250;
            WindowManager.Get(this).Height = 850;
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

        public XamlRoot GetXamlRoot()
        {
            return this.Content.XamlRoot;
        }

        public void GoToSoftwarePage()
        {
            ContentFrame.Navigate(
                   new SoftwarePage().GetType(),
                   null,
                   new EntranceNavigationTransitionInfo()
                   );
            App.XamlRoot = this.Content.XamlRoot;
            NavigationViewControl.Header = "Software downloading";
        }

        private void NavigationViewControl_ItemInvoked(NavigationView sender,
                      NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                ContentFrame.Navigate(typeof(Views.SettingsPage), null, new DrillInNavigationTransitionInfo());
            }
            else if (args.InvokedItemContainer != null && (args.InvokedItemContainer.Tag != null))
            {
                Type newPage = Type.GetType(args.InvokedItemContainer.Tag.ToString());
                ContentFrame.Navigate(
                       newPage,
                       null,
                       new EntranceNavigationTransitionInfo()
                       );
                App.XamlRoot = this.Content.XamlRoot;
            }
        }

        private void NavigationViewControl_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack) ContentFrame.GoBack();
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            NavigationViewControl.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(Views.SettingsPage))
            {
                // SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag.
                NavigationViewControl.SelectedItem = (NavigationViewItem)NavigationViewControl.SettingsItem;
                NavigationViewControl.HeaderTemplate = Application.Current.Resources["OtherHeader"] as DataTemplate;
                ContentFrame.Padding = new Thickness(55, 0, 0, 0);
            }
            if (ContentFrame.SourcePageType == typeof(SoftwarePage))
            {
                // SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag.
                NavigationViewControl.SelectedItem = (NavigationViewItem)NavigationViewControl.SettingsItem;
                NavigationViewControl.HeaderTemplate = Application.Current.Resources["OtherHeader"] as DataTemplate;
                ContentFrame.Padding = new Thickness(55, 0, 0, 0);
            }
            else if(ContentFrame.SourcePageType == typeof(Views.HomePage))
            {
                NavigationViewControl.HeaderTemplate = null;
                NavigationViewControl.Header = null;
                ContentFrame.Padding = new Thickness(0,0,0,0);
                return;
            }
            else if (ContentFrame.SourcePageType != null && ContentFrame.SourcePageType != typeof(Views.SubSection))
            {
                NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(ContentFrame.SourcePageType.FullName.ToString()));
                NavigationViewControl.HeaderTemplate = Application.Current.Resources["OtherHeader"] as DataTemplate;
                ContentFrame.Padding = new Thickness(55, 0, 0, 0);
            }
            NavigationViewControl.Header = ((NavigationViewItem)NavigationViewControl.SelectedItem)?.Content?.ToString();
        }

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
            var screenWidth = GetSystemMetrics(SM_CXSCREEN);
            var screenHeight = GetSystemMetrics(SM_CYSCREEN);

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

    }
}
