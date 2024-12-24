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
    public class TestConfigurationService : IConfigurationService
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\TestConfig";
        private const string STATE_VALUE_NAME = "state";


        private readonly ConfigurationStore _testConfigurationService;

        public TestConfigurationService(
            [FromKeyedServices("TestConfig")] ConfigurationStore testConfigurationService)
        {
            _testConfigurationService = testConfigurationService;
        }

        public void Disable()
        {
            RegistryHelper.DeleteKey(ATLAS_STORE_KEY_NAME);

            _testConfigurationService.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);

            _testConfigurationService.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
        }
    }
}
