using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class SnapLayoutsConfigurationService : IConfigurationService
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\SnapLayouts";
        private const string STATE_VALUE_NAME = "state";

        private const string ADVANCED_KEY_NAME = @"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";

        private const string ENABLE_SNAP_ASSIST_FLYOUT_VALUE_NAME = "EnableSnapAssistFlyout";
        private const string ENABLE_SNAP_BAR_VALUE_NAME = "EnableSnapBar";

        private readonly ConfigurationStore _snapLayoutsConfigurationService;

        public SnapLayoutsConfigurationService(
            [FromKeyedServices("SnapLayout")] ConfigurationStore snapLayoutsConfigurationService)
        {
            _snapLayoutsConfigurationService = snapLayoutsConfigurationService;
        }

        public void Disable()
        {
            RegistryHelper.SetValue(ADVANCED_KEY_NAME, ENABLE_SNAP_ASSIST_FLYOUT_VALUE_NAME, 0000000, RegistryValueKind.DWord);
            RegistryHelper.SetValue(ADVANCED_KEY_NAME, ENABLE_SNAP_BAR_VALUE_NAME, 0000000, RegistryValueKind.DWord);

            RegistryHelper.DeleteKey(ATLAS_STORE_KEY_NAME);

            _snapLayoutsConfigurationService.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.SetValue(ADVANCED_KEY_NAME, ENABLE_SNAP_ASSIST_FLYOUT_VALUE_NAME, 0000001, RegistryValueKind.DWord);
            RegistryHelper.SetValue(ADVANCED_KEY_NAME, ENABLE_SNAP_BAR_VALUE_NAME, 0000001, RegistryValueKind.DWord);

            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);

            _snapLayoutsConfigurationService.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
        }
    }
}
