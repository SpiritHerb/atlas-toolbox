using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using AtlasToolbox.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;
using System.Linq;
using System.Threading;


namespace AtlasToolbox.Services.ConfigurationServices
{
    public class ExtractContextMenuConfigurationService : IConfigurationService
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\AutomaticUpdates";
        private const string STATE_VALUE_NAME = "state";

        private const string AU_KEY_NAME = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU";

        private const string AU_OPTIONS_VALUE_NAME = "AUOptions";

        private readonly ConfigurationStore _extractContextMenuConfigurationService;
        public ExtractContextMenuConfigurationService(
            [FromKeyedServices("ExtractContextMenu")] ConfigurationStore extractContextMenuConfigurationService)
        {
            _extractContextMenuConfigurationService = extractContextMenuConfigurationService;
        }

        public void Disable()
        {
            RegistryHelper.SetValue(AU_KEY_NAME, AU_OPTIONS_VALUE_NAME, 2, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.DeleteKey(ATLAS_STORE_KEY_NAME);

            _extractContextMenuConfigurationService.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.DeleteValue(AU_KEY_NAME, AU_OPTIONS_VALUE_NAME);
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);

            _extractContextMenuConfigurationService.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            bool[] checks =
            {
                RegistryHelper.IsMatch(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1)
            };

            return checks.All(x => x);
        }
    }
}
