using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class RemovableDrivesInSidebarConfigurationService : IConfigurationService
    {
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

            _removableDrivesInSidebarConfigurationStore.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            foreach (string keyName in _keyNames)
            {
                RegistryHelper.SetValue(keyName, string.Empty, "Removable Drives");
            }

            _removableDrivesInSidebarConfigurationStore.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return _keyNames.All(x => RegistryHelper.IsMatch(x, string.Empty, "Removable Drives"));
        }
    }
}
