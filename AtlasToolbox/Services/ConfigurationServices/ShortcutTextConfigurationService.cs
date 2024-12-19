using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class ShortcutTextConfigurationService : IConfigurationService
    {
        private const string EXPLORER_KEY_NAME = @"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer";

        private const string LINK_VALUE_NAME = "link";

        private readonly ConfigurationStore _shortcutTextConfigurationStore;

        public ShortcutTextConfigurationService(
            [FromKeyedServices("ShortcutText")] ConfigurationStore shortcutTextConfigurationStore)
        {
            _shortcutTextConfigurationStore = shortcutTextConfigurationStore;
        }

        public void Disable()
        {
            RegistryHelper.SetValue(EXPLORER_KEY_NAME, LINK_VALUE_NAME, Convert.FromHexString("00000000"));

            _shortcutTextConfigurationStore.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.SetValue(EXPLORER_KEY_NAME, LINK_VALUE_NAME, Convert.FromHexString("15000000"));

            _shortcutTextConfigurationStore.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(EXPLORER_KEY_NAME, LINK_VALUE_NAME, Convert.FromHexString("15000000"));
        }
    }
}
