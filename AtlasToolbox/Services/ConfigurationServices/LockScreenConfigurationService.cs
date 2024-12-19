using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class LockScreenConfigurationService : IConfigurationService
    {
        private const string SESSION_DATA_KEY_NAME = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\SessionData";

        private const string ALLOW_LOCK_SCREEN_VALUE_NAME = "AllowLockScreen";

        private readonly ConfigurationStore _lockScreenConfigurationStore;

        public LockScreenConfigurationService(
            [FromKeyedServices("LockScreen")] ConfigurationStore lockScreenConfigurationStore)
        {
            _lockScreenConfigurationStore = lockScreenConfigurationStore;
        }

        public void Disable()
        {
            RegistryHelper.SetValue(SESSION_DATA_KEY_NAME, ALLOW_LOCK_SCREEN_VALUE_NAME, 0);

            _lockScreenConfigurationStore.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.SetValue(SESSION_DATA_KEY_NAME, ALLOW_LOCK_SCREEN_VALUE_NAME, 1);

            _lockScreenConfigurationStore.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(SESSION_DATA_KEY_NAME, ALLOW_LOCK_SCREEN_VALUE_NAME, 1);
        }
    }
}
