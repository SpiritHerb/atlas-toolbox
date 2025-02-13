using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using AtlasToolbox.Utils;

namespace AtlasToolbox.Utils
{
    public static class SettingsBehaviorHelper
    {
        public static void KeppBackground_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                //keepBackground_IsOn = toggleSwitch.IsOn;
                if (toggleSwitch.IsOn)
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
        }
    }
}
