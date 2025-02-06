using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class MultiOptionTestConfigurationService : IMultiOptionConfigurationServices
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\MultiChoiceTestConfig";
        private const string STATE_VALUE_NAME = "state";

        private readonly MultiOptionConfigurationStore _multiChoiceTestConfigurationService;

        private const string MULTI_CHOICE_REG_FILE_PATH = "C:\\Users\\TheyCreeper\\Documents\\Dev\\AtlasToolbox-WinUI3\\AtlasToolbox\\RegTest\\RegTest_";

        private List<string> options = new List<string>()
        {
            "Test config option 1",
            "Test config option 2",
            "Test config option 3",
        };

        public MultiOptionTestConfigurationService(
            [FromKeyedServices("MultiOption")] MultiOptionConfigurationStore multiChoiceTestConfigurationService)
        {
            _multiChoiceTestConfigurationService = multiChoiceTestConfigurationService;
            _multiChoiceTestConfigurationService.Options = options;
        }

        public void ChangeStatus(int status)
        {
            //int status = options.IndexOf("statusString");
            //RegistryHelper.MergeRegFile(MULTI_CHOICE_REG_FILE_PATH + status.ToString() + ".reg");

            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, status);
            _multiChoiceTestConfigurationService.CurrentSetting = Status();
        }

        public string Status()
        {
            try
            {
                return options[((int)RegistryHelper.GetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME))];
            }
            catch (Exception ex)
            {
                ChangeStatus(0);
                return options[((int)RegistryHelper.GetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME))];

            }
        }
    }
}
