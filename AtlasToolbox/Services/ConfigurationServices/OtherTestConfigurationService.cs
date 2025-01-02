using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class OtherTestConfigurationService : IConfigurationService
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\OtherTestConfig";
        private const string STATE_VALUE_NAME = "state";


        private readonly ConfigurationStore _otherTestConfigurationService;

        public OtherTestConfigurationService(
            [FromKeyedServices("OtherTestConfig")] ConfigurationStore otherTestConfigurationService)
        {
            _otherTestConfigurationService = otherTestConfigurationService;
        }

        public void Disable()
        {
            RegistryHelper.DeleteKey(ATLAS_STORE_KEY_NAME);
            ComputerStateHelper.LogOffComputer();

            _otherTestConfigurationService.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
            ComputerStateHelper.LogOffComputer();

            _otherTestConfigurationService.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
        }
    }
}
