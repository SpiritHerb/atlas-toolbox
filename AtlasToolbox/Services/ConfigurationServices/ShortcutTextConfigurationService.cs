using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class ShortcutTextConfigurationService : IConfigurationService
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\ShortcutText";
        private const string STATE_VALUE_NAME = "state";

        private const string EXPLORER_KEY_NAME = @"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\NamingTemplates";

        private const string SHORTCUT_NAME_TEMPLATE_VALUE_NAME = "ShortcutNameTemplate";

        private readonly ConfigurationStore _shortcutTextConfigurationStore;

        public ShortcutTextConfigurationService(
            [FromKeyedServices("ShortcutText")] ConfigurationStore shortcutTextConfigurationStore)
        {
            _shortcutTextConfigurationStore = shortcutTextConfigurationStore;
        }

        public void Disable()
        {
            RegistryHelper.SetValue(EXPLORER_KEY_NAME, SHORTCUT_NAME_TEMPLATE_VALUE_NAME, "\"%s.lnk\"");
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 0);

            _shortcutTextConfigurationStore.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.DeleteValue(EXPLORER_KEY_NAME, SHORTCUT_NAME_TEMPLATE_VALUE_NAME);
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);

            _shortcutTextConfigurationStore.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
        }
    }
}
