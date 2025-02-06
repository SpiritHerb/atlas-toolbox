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
    public class NvidiaDispayContainerConfigurationService : IConfigurationService
    {

        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\NVidiaDisplayContainer";
        private const string STATE_VALUE_NAME = "state";

        private readonly ConfigurationStore _nvidiaDispayContainerConfigurationService;

        public NvidiaDispayContainerConfigurationService(
            [FromKeyedServices("NvidiaDispayContainer")]  ConfigurationStore nvidiaDispayContainerConfigurationService)
        {
            _nvidiaDispayContainerConfigurationService = nvidiaDispayContainerConfigurationService;
        }
        public void Disable()
        {
            RegistryHelper.DeleteKey(ATLAS_STORE_KEY_NAME);

            CommandPromptHelper.RunCommand(@$"{Environment.GetEnvironmentVariable("windir")}\AtlasModules\Toolbox\Scripts\NVidia\DisableNVIDIADisplayContainerLS.cmd", false);
            _nvidiaDispayContainerConfigurationService.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
            CommandPromptHelper.RunCommand(@$"{Environment.GetEnvironmentVariable("windir")}\AtlasModules\Toolbox\Scripts\NVidia\DisableNVIDIADisplayContainerLS.cmd", false);

            _nvidiaDispayContainerConfigurationService.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
        }
    }
}
