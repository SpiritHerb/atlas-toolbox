using AtlasToolbox.Utils;
using AtlasToolbox.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Windows.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AtlasToolbox.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private bool _toggleSwitchIsOn = RegistryHelper.IsMatch("HKLM\\SOFTWARE\\AtlasOS\\Toolbox", "KeepInBackground", 1);
        public bool ToggleSwitch_IsOn
        {
            get => _toggleSwitchIsOn;
            set
            {
                _toggleSwitchIsOn = value;
                OnToggleSwitchChanged();
            }
        }
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private void OnToggleSwitchChanged()
        {
            if (_toggleSwitchIsOn)
            {
                RegistryHelper.SetValue("HKLM\\SOFTWARE\\AtlasOS\\Toolbox", "KeepInBackground", 1);
                App.m_window.Closed += AppBehaviorHelper.HideApp;
            }
            else
            {
                RegistryHelper.DeleteValue("HKLM\\SOFTWARE\\AtlasOS\\Toolbox", "KeepInBackground");
                App.m_window.Closed += AppBehaviorHelper.CloseApp;
            }
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                ToggleSwitch_IsOn = toggleSwitch.IsOn;
            }
        }
        
    }
}
