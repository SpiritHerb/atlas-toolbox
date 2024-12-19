using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.ServiceProcess;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class MicrosoftStoreConfigurationService : IConfigurationService
    {
        private const string EXPLORER_KEY_NAME = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\Explorer";
        private const string WINDOWS_STORE_KEY_NAME = @"HKLM\SOFTWARE\Policies\Microsoft\WindowsStore";

        private const string NO_USE_STORE_OPEN_WITH_VALUE_NAME = "NoUseStoreOpenWith";
        private const string REMOVE_WINDOWS_STORE_VALUE_NAME = "RemoveWindowsStore";

        private const string WLID_SVC_SERVICE_NAME = "wlidsvc";
        private const string APPX_SVC_SERVICE_NAME = "AppXSvc";
        private const string CLIP_SVC_SERVICE_NAME = "ClipSVC";
        private const string FILE_CRYPT_SERVICE_NAME = "FileCrypt";
        private const string FILE_INFO_SERVICE_NAME = "FileInfo";
        private const string INSTALL_SERVICE_SERVICE_NAME = "InstallService";
        private const string LICENSE_MANAGER_SERVICE_NAME = "LicenseManager";
        private const string TOKEN_BROKER_SERVICE_NAME = "TokenBroker";
        private const string WIN_HTTP_AUTO_PROXY_SVC_SERVICE_NAME = "WinHttpAutoProxySvc";

        private readonly ConfigurationStore _microsoftStoreConfigurationStore;

        public MicrosoftStoreConfigurationService(
            [FromKeyedServices("MicrosoftStore")] ConfigurationStore microsoftStoreConfigurationStore)
        {
            _microsoftStoreConfigurationStore = microsoftStoreConfigurationStore;
        }

        public void Disable()
        {
            RegistryHelper.SetValue(EXPLORER_KEY_NAME, NO_USE_STORE_OPEN_WITH_VALUE_NAME, 1);
            RegistryHelper.SetValue(WINDOWS_STORE_KEY_NAME, REMOVE_WINDOWS_STORE_VALUE_NAME, 1);

            ServiceHelper.SetStartupType(APPX_SVC_SERVICE_NAME, ServiceStartMode.Disabled);
            ServiceHelper.SetStartupType(CLIP_SVC_SERVICE_NAME, ServiceStartMode.Disabled);
            ServiceHelper.SetStartupType(FILE_CRYPT_SERVICE_NAME, ServiceStartMode.Disabled);
            ServiceHelper.SetStartupType(FILE_INFO_SERVICE_NAME, ServiceStartMode.Disabled);
            ServiceHelper.SetStartupType(INSTALL_SERVICE_SERVICE_NAME, ServiceStartMode.Disabled);
            ServiceHelper.SetStartupType(LICENSE_MANAGER_SERVICE_NAME, ServiceStartMode.Disabled);
            ServiceHelper.SetStartupType(TOKEN_BROKER_SERVICE_NAME, ServiceStartMode.Disabled);
            ServiceHelper.SetStartupType(WIN_HTTP_AUTO_PROXY_SVC_SERVICE_NAME, ServiceStartMode.Disabled);

            if (!UserAccountHelper.MicrosoftAccountExists())
            {
                ServiceHelper.SetStartupType(WLID_SVC_SERVICE_NAME, ServiceStartMode.Disabled);
            }

            _microsoftStoreConfigurationStore.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.DeleteValue(EXPLORER_KEY_NAME, NO_USE_STORE_OPEN_WITH_VALUE_NAME);
            RegistryHelper.DeleteValue(WINDOWS_STORE_KEY_NAME, REMOVE_WINDOWS_STORE_VALUE_NAME);

            ServiceHelper.SetStartupType(APPX_SVC_SERVICE_NAME, ServiceStartMode.Manual);
            ServiceHelper.SetStartupType(CLIP_SVC_SERVICE_NAME, ServiceStartMode.Manual);
            ServiceHelper.SetStartupType(FILE_CRYPT_SERVICE_NAME, ServiceStartMode.System);
            ServiceHelper.SetStartupType(FILE_INFO_SERVICE_NAME, ServiceStartMode.Boot);
            ServiceHelper.SetStartupType(INSTALL_SERVICE_SERVICE_NAME, ServiceStartMode.Manual);
            ServiceHelper.SetStartupType(LICENSE_MANAGER_SERVICE_NAME, ServiceStartMode.Manual);
            ServiceHelper.SetStartupType(TOKEN_BROKER_SERVICE_NAME, ServiceStartMode.Manual);
            ServiceHelper.SetStartupType(WIN_HTTP_AUTO_PROXY_SVC_SERVICE_NAME, ServiceStartMode.Manual);
            ServiceHelper.SetStartupType(WLID_SVC_SERVICE_NAME, ServiceStartMode.Manual);

            _microsoftStoreConfigurationStore.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            bool[] checks =
            {
                RegistryHelper.IsMatch(EXPLORER_KEY_NAME, NO_USE_STORE_OPEN_WITH_VALUE_NAME, null),
                RegistryHelper.IsMatch(WINDOWS_STORE_KEY_NAME, REMOVE_WINDOWS_STORE_VALUE_NAME, null),
                ServiceHelper.IsStartupTypeMatch(APPX_SVC_SERVICE_NAME, ServiceStartMode.Manual),
                ServiceHelper.IsStartupTypeMatch(CLIP_SVC_SERVICE_NAME, ServiceStartMode.Manual),
                ServiceHelper.IsStartupTypeMatch(FILE_CRYPT_SERVICE_NAME, ServiceStartMode.System),
                ServiceHelper.IsStartupTypeMatch(FILE_INFO_SERVICE_NAME, ServiceStartMode.Boot),
                ServiceHelper.IsStartupTypeMatch(INSTALL_SERVICE_SERVICE_NAME, ServiceStartMode.Manual),
                ServiceHelper.IsStartupTypeMatch(LICENSE_MANAGER_SERVICE_NAME, ServiceStartMode.Manual),
                ServiceHelper.IsStartupTypeMatch(TOKEN_BROKER_SERVICE_NAME, ServiceStartMode.Manual),
                ServiceHelper.IsStartupTypeMatch(WIN_HTTP_AUTO_PROXY_SVC_SERVICE_NAME, ServiceStartMode.Manual),
                ServiceHelper.IsStartupTypeMatch(WLID_SVC_SERVICE_NAME, ServiceStartMode.Manual)
            };

            return checks.All(x => x);
        }
    }
}
