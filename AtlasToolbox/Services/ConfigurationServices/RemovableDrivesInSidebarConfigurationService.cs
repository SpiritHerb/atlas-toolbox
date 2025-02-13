using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class RemovableDrivesInSidebarConfigurationService : IConfigurationService
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\RemovableDrivesInSidebar";
        private const string STATE_VALUE_NAME = "state";


        private readonly ConfigurationStore _removableDrivesInSidebarConfigurationStore;
        private readonly IEnumerable<string> _keyNames;

        public RemovableDrivesInSidebarConfigurationService(
            [FromKeyedServices("RemovableDrivesInSidebar")] ConfigurationStore removableDrivesInSidebarConfigurationStore)
        {
            _removableDrivesInSidebarConfigurationStore = removableDrivesInSidebarConfigurationStore;
            _keyNames = new string[]
            {
                @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Desktop\NameSpace\DelegateFolders\{F5FB2C77-0E2F-4A16-A381-3E560C68BC83}",
                @"HKLM\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Explorer\Desktop\NameSpace\DelegateFolders\{F5FB2C77-0E2F-4A16-A381-3E560C68BC83}\{F5FB2C77-0E2F-4A16-A381-3E560C68BC83}"
            };
        }

        public void Disable()
        {
            foreach (string keyName in _keyNames)
            {
                RegistryHelper.DeleteKey(keyName);
            }

            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 0);

            _removableDrivesInSidebarConfigurationStore.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            foreach (string keyName in _keyNames)
            {
                RegistryHelper.SetValue(keyName, string.Empty, "Removable Drives");
            }
                RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);

            _removableDrivesInSidebarConfigurationStore.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
        }
    }
}
