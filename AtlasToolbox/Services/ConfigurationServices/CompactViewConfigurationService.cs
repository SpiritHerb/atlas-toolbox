using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class CompactViewConfigurationService : IConfigurationService
    {
        private const string ADVANCED_KEY_NAME = @"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced";
        private const string USE_COMPACT_MODE_VALUE_NAME = "UseCompactMode";

        private readonly ConfigurationStore _compactViewConfigurationStore;

        public CompactViewConfigurationService(
            [FromKeyedServices("CompactView")] ConfigurationStore compactViewConfigurationStore)
        {
            _compactViewConfigurationStore = compactViewConfigurationStore;
        }

        public void Disable()
        {
            RegistryHelper.SetValue(ADVANCED_KEY_NAME, USE_COMPACT_MODE_VALUE_NAME, 0);

            _compactViewConfigurationStore.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.SetValue(ADVANCED_KEY_NAME, USE_COMPACT_MODE_VALUE_NAME, 1);

            _compactViewConfigurationStore.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(ADVANCED_KEY_NAME, USE_COMPACT_MODE_VALUE_NAME, 1);
        }
    }
}
