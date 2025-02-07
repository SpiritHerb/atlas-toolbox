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
    class RecalSupportConfigurationService : IConfigurationService
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\Recal";
        private const string STATE_VALUE_NAME = "state";

        private const string WINDOWS_AI_KEY_NAME = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsAI";

        private readonly ConfigurationStore _recalConfigurationStore;

        public RecalSupportConfigurationService([FromKeyedServices("Recal")] ConfigurationStore recalConfigurationStore)
        {
            _recalConfigurationStore = recalConfigurationStore;
        }

        public void Disable()
        {
            RegistryHelper.DeleteKey(ATLAS_STORE_KEY_NAME);
            RegistryHelper.SetValue(WINDOWS_AI_KEY_NAME, "DisableAIDataAnalysis", 000000001, Microsoft.Win32.RegistryValueKind.DWord);
            _recalConfigurationStore.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
            RegistryHelper.DeleteValue(WINDOWS_AI_KEY_NAME, "DisableAIDataAnalysis");

            _recalConfigurationStore.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
        }
    }
}
