using AtlasToolbox.Utils;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AtlasToolbox.Views
{
    public sealed partial class SettingsPage : Page
    {
        public bool KeepBackground_State = RegistryHelper.IsMatch("HKLM\\SOFTWARE\\AtlasOS\\Toolbox", "KeepInBackground", 1);

        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private void KeepBackground_Toggled(object sender, RoutedEventArgs e)
        {
            SettingsBehaviorHelper.KeppBackground_Toggled(sender, e);
        }

    }
}
