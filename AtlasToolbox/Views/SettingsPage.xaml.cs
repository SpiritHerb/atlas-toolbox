using System;
using System.Configuration;
using AtlasToolbox.Utils;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;

namespace AtlasToolbox.Views
{
    public sealed partial class SettingsPage : Page
    {
        public bool KeepBackground_State = RegistryHelper.IsMatch("HKLM\\SOFTWARE\\AtlasOS\\Toolbox", "KeepInBackground", 1);

        public string Version
        {
            get
            {
                return App.Version;
            }
        }

        public SettingsPage()
        {
            this.InitializeComponent();
            LoadText();
        }

        private void LoadText()
        {
            TitleTxt.Text = App.GetValueFromItemList("Settings");
            BehaviorHeader.Text = App.GetValueFromItemList("Behavior");
            BackgroundDescription.Header = App.GetValueFromItemList("Settings_BackgroundDesc");
            AboutHeader.Text = App.GetValueFromItemList("About");
            toCloneRepoCard.Header = App.GetValueFromItemList("CloneRepoCard");
            bugRequestCard.Header = App.GetValueFromItemList("BugReportCard");
            WarningHeader.Header = App.GetValueFromItemList("WarningHeader");
        }

        private void KeepBackground_Toggled(object sender, RoutedEventArgs e)
        {
            SettingsBehaviorHelper.KeppBackground_Toggled(sender, e);
        }

        private void toCloneRepoCard_Click(object sender, RoutedEventArgs e)
        {
            DataPackage package = new DataPackage();
            package.SetText(gitCloneTextBlock.Text);
            Clipboard.SetContent(package);
        }

        private async void bugRequestCard_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://github.com/Atlas-OS/atlas-toolbox/issues/new"));
        }
    }
}
