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
    public class MultiChoiceTestConfigurationService : IMultiOptionConfigurationServices
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\MultiChoiceTestConfig";
        private const string STATE_VALUE_NAME = "state";


        private readonly MultiOptionConfigurationStore _multiChoiceTestConfigurationService;

        private const string MULTI_CHOICE_REG_FILE_PATH = "C:\\Users\\TheyCreeper\\Documents\\Dev\\AtlasToolbox-WinUI3\\AtlasToolbox\\RegTest\\RegTest_";

        public MultiChoiceTestConfigurationService(
            [FromKeyedServices("MultiChoiceTestConfig")] MultiOptionConfigurationStore multiChoiceTestConfigurationService)
        {
            _multiChoiceTestConfigurationService = multiChoiceTestConfigurationService;
        }

        public void ChangeStatus(int status)
        {
            RegistryHelper.MergeRegFile(MULTI_CHOICE_REG_FILE_PATH + status.ToString() + ".reg");

            if (status == 0)
            {
                RegistryHelper.DeleteKey(ATLAS_STORE_KEY_NAME);
            }
            else
            {
                RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, status);
            };
            _multiChoiceTestConfigurationService.CurrentSetting = Status();
        }

        public byte Status()
        {
            return (byte)RegistryHelper.GetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME);
        }
    }
}
