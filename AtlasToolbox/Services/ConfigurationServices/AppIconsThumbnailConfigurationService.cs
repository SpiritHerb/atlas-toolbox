using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class AppIconsThumbnailConfigurationService : IConfigurationService
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\AppIconsThumbnail";
        private const string STATE_VALUE_NAME = "state";

        private const string ADVANCED_KEY_NAME = @"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";

        private const string SHOW_TYPE_OVERLAY_KEY_NAME = "ShowTypeOverlay";

        private readonly ConfigurationStore _appIconsThumbnailConfigurationService;

        public AppIconsThumbnailConfigurationService(
            [FromKeyedServices("AppIconsThumbnail")] ConfigurationStore appIconsThumbnailConfigurationService)
        {
            _appIconsThumbnailConfigurationService = appIconsThumbnailConfigurationService;
        }

        public void Disable()
        {
            RegistryHelper.SetValue(ADVANCED_KEY_NAME, SHOW_TYPE_OVERLAY_KEY_NAME, 00000000, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.DeleteKey(ATLAS_STORE_KEY_NAME);

            _appIconsThumbnailConfigurationService.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.SetValue(ADVANCED_KEY_NAME, SHOW_TYPE_OVERLAY_KEY_NAME, 00000001, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);

            _appIconsThumbnailConfigurationService.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return RegistryHelper.IsMatch(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, 1);
        }
    }
}
