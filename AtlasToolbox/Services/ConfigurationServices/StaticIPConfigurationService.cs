using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Models;
using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class StaticIPConfigurationService : IConfigurationService
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\StaticIp";
        private const string STATE_VALUE_NAME = "state";

        private readonly ConfigurationStore _staticIPConfigurationService;
        public StaticIPConfigurationService(
            [FromKeyedServices("StaticIp")] ConfigurationStore staticIPConfigurationService)
        {
            _staticIPConfigurationService = staticIPConfigurationService;
        }

        public void Disable()
        {
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 0);
            CommandPromptHelper.RunCommand(@$"{Environment.GetEnvironmentVariable("windir")}\AtlasModules\Toolbox\Scripts\StaticIP\RevertStaticIP.cmd");

            _staticIPConfigurationService.CurrentSetting = IsEnabled();
            App.ContentDialogCaller("restart");
        }

        public void Enable()
        {
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
            CommandPromptHelper.RunCommand(@$"{Environment.GetEnvironmentVariable("windir")}\AtlasModules\Toolbox\Scripts\StaticIP\AutomaticallySetStaticIP.cmd");

            _staticIPConfigurationService.CurrentSetting = IsEnabled();
            App.ContentDialogCaller("restart");
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
        }
    }
}
