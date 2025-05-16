using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using AtlasToolbox.Utils;
using AtlasToolbox.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Newtonsoft.Json;
using NLog.LayoutRenderers;
using Windows.ApplicationModel.Appointments;
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
            this.DataContext = new SettingsPageViewModel();
            LoadText();
            ConfigSwitch.Loaded += (s, e) =>
            {
                ConfigSwitch.SelectionChanged += ConfigSwitch_SelectionChanged;
;
            };

        }

        public void LoadText()
        {
            TitleTxt.Text = App.GetValueFromItemList("Settings");
            BehaviorHeader.Text = App.GetValueFromItemList("Behavior");
            BackgroundDescription.Header = App.GetValueFromItemList("Settings_BackgroundDesc");
            AboutHeader.Text = App.GetValueFromItemList("About");
            toCloneRepoCard.Header = App.GetValueFromItemList("CloneRepoCard");
            bugRequestCard.Header = App.GetValueFromItemList("BugReportCard");
            WarningHeader.Header = App.GetValueFromItemList("WarningHeader");
            LanguageHeader.Header = App.GetValueFromItemList("Language");
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

        private void ConfigSwitch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.ContentDialogCaller("restartApp");
        }
    }
}
