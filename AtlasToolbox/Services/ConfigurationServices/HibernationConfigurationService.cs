using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace AtlasToolbox.Services.ConfigurationServices
{
    internal class HibernationConfigurationService : IConfigurationService
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\Hibernation";
        private const string STATE_VALUE_NAME = "state";

        private readonly ConfigurationStore _hibernationConfigurationStore;
        public HibernationConfigurationService(
            [FromKeyedServices("Hibernation")] ConfigurationStore hibernationConfigurationStore)
        {
            _hibernationConfigurationStore = hibernationConfigurationStore;
        }
        public void Disable()
        {
            CommandPromptHelper.RunCommand("powercfg /h on");
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 0);
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, "path", @$"{Environment.GetEnvironmentVariable("windir")}\AtlasDesktop\3. General Configuration\Hibernation\Disable Hibernation (default).cmd");

            _hibernationConfigurationStore.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            CommandPromptHelper.RunCommand("powercfg /h off");

            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, "path", @$"{Environment.GetEnvironmentVariable("windir")}\AtlasDesktop\3. General Configuration\Hibernation\Enable Hibernation.cmd");

            _hibernationConfigurationStore.CurrentSetting = IsEnabled();
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
