using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;

namespace AtlasToolbox.Utils
{
    public static class SettingsBehaviorHelper
    {
        /// <summary>
        /// Changes the background setting in the registry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void KeppBackground_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
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
