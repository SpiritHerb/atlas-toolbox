using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class QuickAccessConfigurationService : IConfigurationService
    {
        private const string EXPLORER_KEY_NAME = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer";

        private const string HUB_MODE_VALUE_NAME = "HubMode";

        private readonly ConfigurationStore _quickAccessConfigurationStore;

        public QuickAccessConfigurationService(
            [FromKeyedServices("QuickAccess")] ConfigurationStore quickAccessConfigurationStore)
        {
            _quickAccessConfigurationStore = quickAccessConfigurationStore;
        }

        public void Disable()
        {
            RegistryHelper.SetValue(EXPLORER_KEY_NAME, HUB_MODE_VALUE_NAME, 1);

            _quickAccessConfigurationStore.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.DeleteValue(EXPLORER_KEY_NAME, HUB_MODE_VALUE_NAME);

            _quickAccessConfigurationStore.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(EXPLORER_KEY_NAME, HUB_MODE_VALUE_NAME, null);
        }
    }
}
