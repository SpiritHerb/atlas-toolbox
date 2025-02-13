using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class HideAppBrowserControlConfigurationService : IConfigurationService
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\HideAppBrowserControl";
        private const string STATE_VALUE_NAME = "state";

        private const string APP_BROWSER_PROTECTION_KEY_NAME = @"HKLM\SOFTWARE\Policies\Microsoft\Windows Defender Security Center\App and Browser protection";

        private readonly ConfigurationStore _hideAppBrowserControlConfigurationService;

        public HideAppBrowserControlConfigurationService(
            [FromKeyedServices("HideAppBrowserControl")] ConfigurationStore hideAppBrowserControlConfigurationService)
        {
            _hideAppBrowserControlConfigurationService = hideAppBrowserControlConfigurationService;
        }

        public void Disable()
        {
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 0);
            RegistryHelper.SetValue(APP_BROWSER_PROTECTION_KEY_NAME, "UILockdown", 1, RegistryValueKind.DWord);

            _hideAppBrowserControlConfigurationService.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
            RegistryHelper.DeleteValue(APP_BROWSER_PROTECTION_KEY_NAME, "UILockdown");

            _hideAppBrowserControlConfigurationService.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
        }
    }
}
