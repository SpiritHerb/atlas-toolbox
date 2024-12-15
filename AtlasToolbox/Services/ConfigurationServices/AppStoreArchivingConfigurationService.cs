using Microsoft.Extensions.DependencyInjection;
using AtlasToolbox.Stores;
using AtlasToolbox.Utils;

namespace AtlasToolbox.Services.ConfigurationServices
{
    internal class AppStoreArchivingConfigurationService : IConfigurationService
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\AppStoreArchiving";
        private const string STATE_VALUE_NAME = "state";

        private const string APPX_KEY_NAME = @"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Appx";

        private const string ALLOW_AUTOMATIC_APP_ARCHIVING_VALUE_NAME = "AllowAutomaticAppArchiving";

        private readonly ConfigurationStore _appStoreArchivingConfigurationService;

        public AppStoreArchivingConfigurationService() { }
        public AppStoreArchivingConfigurationService(
            [FromKeyedServices("AppStoreArchiving")] ConfigurationStore appStoreArchivingConfigurationStore)
        {
            _appStoreArchivingConfigurationService = appStoreArchivingConfigurationStore;
        }

        public void Disable()
        {
            RegistryHelper.SetValue(APPX_KEY_NAME, ALLOW_AUTOMATIC_APP_ARCHIVING_VALUE_NAME, 0, Microsoft.Win32.RegistryValueKind.DWord);

            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 0);

            _appStoreArchivingConfigurationService.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.SetValue(APPX_KEY_NAME, ALLOW_AUTOMATIC_APP_ARCHIVING_VALUE_NAME, 1, Microsoft.Win32.RegistryValueKind.DWord);

            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);

            _appStoreArchivingConfigurationService.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
        }
    }
}
