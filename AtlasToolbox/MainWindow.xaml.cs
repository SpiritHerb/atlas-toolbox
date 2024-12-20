using System;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Composition.SystemBackdrops;
using System.Collections.ObjectModel;
using AtlasToolbox.Views;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using Windows.Graphics;
using Microsoft.UI.Xaml.Interop;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AtlasToolbox
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private AppWindow _apw;
        private OverlappedPresenter _presenter;

        public MainWindow()
        {
            this.InitializeComponent();

            IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this); // Assuming 'this' is your Window instance

            var windowId = Win32Interop.GetWindowIdFromWindow(hwnd);

            AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

            WindowManager.Get(this).IsMaximizable = false;
            WindowManager.Get(this).IsResizable = false;
            WindowManager.Get(this).Width = 1250;
            WindowManager.Get(this).Height = 850;

            NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems.OfType<NavigationViewItem>().First();
            ContentFrame.Navigate(
                       typeof(Views.HomePage),
                       null,
                       new Microsoft.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo()
                       );

            SystemBackdrop = new MicaBackdrop()
            { Kind = MicaKind.Base };

            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
            TrySetDesktopAcrylicBackdrop();

        }

        // Option 2 - Implement Acrylic with codebehind.
        // Allows for toggling backdrops as shown in sample.
        bool TrySetDesktopAcrylicBackdrop()
        {
            if (Microsoft.UI.Composition.SystemBackdrops.MicaController.IsSupported())
            {
                Microsoft.UI.Xaml.Media.MicaBackdrop micaBackdrop = new Microsoft.UI.Xaml.Media.MicaBackdrop();
                this.SystemBackdrop = micaBackdrop;

                return true; // Succeeded.
            }

            return false; // DesktopAcrylic is not supported on this system.
        }
        private void NavigationViewControl_ItemInvoked(NavigationView sender,
                      NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                ContentFrame.Navigate(typeof(Views.SettingsPage), null, args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null && (args.InvokedItemContainer.Tag != null))
            {
                Type newPage = Type.GetType(args.InvokedItemContainer.Tag.ToString());
                ContentFrame.Navigate(
                       newPage,
                       null,
                       args.RecommendedNavigationTransitionInfo
                       );
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
            }
            else if(ContentFrame.SourcePageType == typeof(Views.HomePage))
            {
                NavigationViewControl.HeaderTemplate = Application.Current.Resources["HeaderHome"] as DataTemplate;
            }
            else if (ContentFrame.SourcePageType != null && ContentFrame.SourcePageType != typeof(Views.SubSection))
            {
                NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(ContentFrame.SourcePageType.FullName.ToString()));
                NavigationViewControl.HeaderTemplate = Application.Current.Resources["OtherHeader"] as DataTemplate;

            }
            NavigationViewControl.Header = ((NavigationViewItem)NavigationViewControl.SelectedItem)?.Content?.ToString();
        }
    }
}
