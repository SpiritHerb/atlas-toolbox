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
    public class VerboseStatusMessageConfiguarationServices : IConfigurationService
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\VerboseStatusMessage";
        private const string STATE_VALUE_NAME = "state";

        private const string SYSTEM_POLICIES_KEY_NAME = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System";
        private const string VERBOSE_STATUS = "verbosestatus";

        private readonly ConfigurationStore _verboseStatusMessageConfigurationService;

        public VerboseStatusMessageConfiguarationServices(
            [FromKeyedServices("VerboseStatusMessage")] ConfigurationStore verboseStatusMessageConfigurationService)
        {
            _verboseStatusMessageConfigurationService = verboseStatusMessageConfigurationService;
        }
        public void Disable()
        {
            RegistryHelper.DeleteValue(SYSTEM_POLICIES_KEY_NAME, VERBOSE_STATUS);
            RegistryHelper.DeleteKey(ATLAS_STORE_KEY_NAME);

            _verboseStatusMessageConfigurationService.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.SetValue(SYSTEM_POLICIES_KEY_NAME, VERBOSE_STATUS, 00000001, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);

            _verboseStatusMessageConfigurationService.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
        }
    }
}
