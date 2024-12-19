using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace AtlasToolbox.Services.ConfigurationServices
{
    internal class SystemRestoreConfigurationService : IConfigurationService
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\SystemRestore";
        private const string STATE_VALUE_NAME = "state";

        private const string SYSTEM_RESTORE_KEY_NAME = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore";

        private const string DISABLE_SR_VALUE_NAME = "DisableSR";

        private readonly ConfigurationStore _systemRestoreConfigurationService;

        public SystemRestoreConfigurationService(
            [FromKeyedServices("SystemRestore")] ConfigurationStore systemRestoreConfigurationService)
        {
            _systemRestoreConfigurationService = systemRestoreConfigurationService;
        }

        public void Disable()
        {
            RegistryHelper.SetValue(SYSTEM_RESTORE_KEY_NAME, DISABLE_SR_VALUE_NAME, 1, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 0);

            _systemRestoreConfigurationService.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.DeleteValue(SYSTEM_RESTORE_KEY_NAME, DISABLE_SR_VALUE_NAME);
            RegistryHelper.DeleteValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME);

            _systemRestoreConfigurationService.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
        }
    }
}
