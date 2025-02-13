using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class NetworkNavigationPaneConfigurationService : IConfigurationService
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\NetworkNavigationPane";
        private const string STATE_VALUE_NAME = "state";

        private const string KEY_KEY_NAME = @"HKEY_CURRENT_USER\SOFTWARE\Classes\CLSID\{F02C1A0D-BE21-4350-88B0-7367FC96EF3C}";
        private const string SYSTEM_PINNED_VALUE_NAME = "System.IsPinnedToNameSpaceTree";
        private readonly ConfigurationStore _configurationStore;

        public NetworkNavigationPaneConfigurationService(
            [FromKeyedServices("NetworkNavigationPane")] ConfigurationStore configurationStore)
        {
            _configurationStore = configurationStore;
        }

        public void Disable()
        {
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 0);
            RegistryHelper.SetValue(KEY_KEY_NAME, SYSTEM_PINNED_VALUE_NAME, "0", Microsoft.Win32.RegistryValueKind.DWord);
        }

        public void Enable()
        {
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
            RegistryHelper.DeleteValue(KEY_KEY_NAME, SYSTEM_PINNED_VALUE_NAME);
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
        }
    }
}
