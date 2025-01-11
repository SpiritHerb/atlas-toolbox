using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using AtlasToolbox.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WindowsDisplayAPI;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AtlasToolbox
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ControlDialogWindowHelper : Window
    {
        private XamlRoot xamlRoot;
        public ControlDialogWindowHelper()
        {
            this.InitializeComponent();
            var displays = Display.GetDisplays();
            WindowManager.Get(this).IsTitleBarVisible = false;
            WindowManager.Get(this).IsAlwaysOnTop = true;
            foreach (Display display in Display.GetDisplays())
            {
                WindowManager.Get(this).MaxHeight = display.CurrentSetting.Resolution.Height;
                WindowManager.Get(this).Width = display.CurrentSetting.Resolution.Width;
            }
            ContentFrame.Navigate(
                       typeof(ControlDialogView),
                       null,
                       new EntranceNavigationTransitionInfo()
                       );

        }

        public void ContentDialogHelper(string contentDialogType)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Do you really wish to delete this profile?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary,
                XamlRoot = xamlRoot
            };

            dialog.ShowAsync().AsTask();
        }

        private void ActiveControlDialogSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ContentDialogHelper("Test");
        }
    }
}
