using System;
using System.Threading;
using System.Threading.Tasks;
using AtlasToolbox.Utils;
using AtlasToolbox.Views;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MVVMEssentials.Commands;

namespace AtlasToolbox.Commands.ConfigurationButtonsCommand
{
    public class SetUpdateDeferalConfigurationButton : AsyncCommandBase
    {
        protected override async Task ExecuteAsync(object parameter)
        {
            UpdateDeferalPage page = new UpdateDeferalPage();
            await Task.Run(() =>
            {
                // stupid hack idk why microsoft doesn't allow us to do a DispatcherQueue.EnqueueAsync
                // in things other than pages and whatnot
                page.ShowUpdateDeferalPrompt();
            });
        }

        public static void SetUpdateDeferal(int featureDays, int qualityDays)
        {
            const string WINDOWS_UPDATE_KEY = "HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate";
            
            RegistryHelper.SetValue(WINDOWS_UPDATE_KEY, "DeferFeatureUpdates", 1, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue(WINDOWS_UPDATE_KEY, "DeferFeatureUpdatesPeriodInDays", featureDays, Microsoft.Win32.RegistryValueKind.DWord);

            RegistryHelper.SetValue(WINDOWS_UPDATE_KEY, "DeferQualityUpdates", 1, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue(WINDOWS_UPDATE_KEY, "DeferQualityUpdatesPeriodInDays", qualityDays, Microsoft.Win32.RegistryValueKind.DWord);

            RegistryHelper.SetValue("HKLM\\SOFTWARE\\AtlasOS\\FeatureUpdateDeferrals", "state", 1, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue("HKLM\\SOFTWARE\\AtlasOS\\FeatureUpdateDeferrals", "path", "C:\\Windows\\AtlasDesktop\\3. General Configuration\\Windows Updates\\Set Windows Update Deferral.cmd", Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue("HKLM\\SOFTWARE\\AtlasOS\\FeatureUpdateDeferrals", "value", featureDays, Microsoft.Win32.RegistryValueKind.DWord);
                                                                                        
            RegistryHelper.SetValue("HKLM\\SOFTWARE\\AtlasOS\\QualityUpdateDeferrals", "state", 1, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue("HKLM\\SOFTWARE\\AtlasOS\\QualityUpdateDeferrals", "path", "C:\\Windows\\AtlasDesktop\\3. General Configuration\\Windows Updates\\Set Windows Update Deferral.cmd", Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue("HKLM\\SOFTWARE\\AtlasOS\\QualityUpdateDeferrals", "value", qualityDays, Microsoft.Win32.RegistryValueKind.DWord);
        }
    }
}
