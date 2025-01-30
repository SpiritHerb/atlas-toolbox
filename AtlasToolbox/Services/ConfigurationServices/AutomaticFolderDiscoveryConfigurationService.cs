using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class AutomaticFolderDiscoveryConfigurationService : IConfigurationService
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\AutomaticFolderDiscovery";
        private const string STATE_VALUE_NAME = "state";

        private const string SHELL_KEY_NAME = @"HKCU\Software\Classes\Local Settings\Software\Microsoft\Windows\Shell\Bags\AllFolders\Shell";

        private const string FOLDER_TYPE_VALUE_NAME = "FolderType";

        private readonly ConfigurationStore _automaticFolderDiscoveryConfigurationService;

        public AutomaticFolderDiscoveryConfigurationService(
            [FromKeyedServices("AutomaticFolderDiscovery")] ConfigurationStore automaticFolderDiscoveryConfigurationService)
        {
            _automaticFolderDiscoveryConfigurationService = automaticFolderDiscoveryConfigurationService;
        }

        public void Disable()
        {
            RegistryHelper.DeleteValue(SHELL_KEY_NAME, FOLDER_TYPE_VALUE_NAME);
            RegistryHelper.DeleteKey(ATLAS_STORE_KEY_NAME);

            _automaticFolderDiscoveryConfigurationService.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.SetValue(SHELL_KEY_NAME, FOLDER_TYPE_VALUE_NAME, 00000000, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);

            _automaticFolderDiscoveryConfigurationService.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
        }
    }
}
