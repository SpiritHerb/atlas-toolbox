using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Utils;
using MVVMEssentials.Commands;

namespace AtlasToolbox.Commands.ConfigurationButtonsCommand
{
    public class ResetWindowsUpdateDeferral : AsyncCommandBase
    {
        protected override async Task ExecuteAsync(object parameter)
        {
            await Task.Run(() =>
            {
                ResetUpdateDeferral();
            });
        }

        private void ResetUpdateDeferral()
        {
            const string WINDOWS_UPDATE_KEY = "HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate";
            const string FEATURE_UDATE_Deferral_KEY = "HKLM\\SOFTWARE\\AtlasOS\\FeatureUpdateDeferrals";
            const string QUALITY_UPDATE_DeferralS_KEY = "HKLM\\SOFTWARE\\AtlasOS\\QualityUpdateDeferrals";

            RegistryHelper.DeleteValue(WINDOWS_UPDATE_KEY, "DeferFeatureUpdates");
            RegistryHelper.DeleteValue(WINDOWS_UPDATE_KEY, "DeferFeatureUpdatesPeriodInDays");

            RegistryHelper.DeleteValue(WINDOWS_UPDATE_KEY, "DeferQualityUpdates");
            RegistryHelper.DeleteValue(WINDOWS_UPDATE_KEY, "DeferQualityUpdatesPeriodInDays");

            RegistryHelper.SetValue(FEATURE_UDATE_Deferral_KEY, "state", 0, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue(FEATURE_UDATE_Deferral_KEY, "path", "C:\\Windows\\AtlasDesktop\\3. General Configuration\\Windows Updates\\Set Windows Update Deferral.cmd", Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue(FEATURE_UDATE_Deferral_KEY, "value", 0, Microsoft.Win32.RegistryValueKind.DWord);

            RegistryHelper.SetValue(QUALITY_UPDATE_DeferralS_KEY, "state", 0, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue(QUALITY_UPDATE_DeferralS_KEY, "path", "C:\\Windows\\AtlasDesktop\\3. General Configuration\\Windows Updates\\Set Windows Update Deferral.cmd", Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue(QUALITY_UPDATE_DeferralS_KEY, "value", 0, Microsoft.Win32.RegistryValueKind.DWord);
        }
    }
}
