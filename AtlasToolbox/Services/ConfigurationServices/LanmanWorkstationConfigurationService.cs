using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Dism;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.ServiceProcess;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class LanmanWorkstationConfigurationService : IConfigurationService
    {
        private readonly ConfigurationStore _lanmanWorkstationConfigurationStore;
        private readonly IDismService _dismService;

        private const string KSECPKG_SERVICE_NAME = "KSecPkg";
        private const string LANMANSERVER_SERVICE_NAME = "LanmanServer";
        private const string LANMANWORKSTATION_SERVICE_NAME = "LanmanWorkstation";
        private const string MRXSMB_SERVICE_NAME = "mrxsmb";
        private const string MRXSMB20_SERVICE_NAME = "mrxsmb20";
        private const string RDBSS_SERVICE_NAME = "rdbss";
        private const string SRV2_SERVICE_NAME = "srv2";

        private const string SMB_DIRECT_FEATURE_NAME = "SmbDirect";

        public LanmanWorkstationConfigurationService(
            [FromKeyedServices("LanmanWorkstation")] ConfigurationStore lanmanWorkstationConfigurationStore,
            IDismService dismService)
        {
            _lanmanWorkstationConfigurationStore = lanmanWorkstationConfigurationStore;
            _dismService = dismService;
        }

        public void Disable()
        {
            ServiceHelper.SetStartupType(KSECPKG_SERVICE_NAME, ServiceStartMode.Disabled);
            ServiceHelper.SetStartupType(LANMANSERVER_SERVICE_NAME, ServiceStartMode.Disabled);
            ServiceHelper.SetStartupType(LANMANWORKSTATION_SERVICE_NAME, ServiceStartMode.Disabled);
            ServiceHelper.SetStartupType(MRXSMB_SERVICE_NAME, ServiceStartMode.Disabled);
            ServiceHelper.SetStartupType(MRXSMB20_SERVICE_NAME, ServiceStartMode.Disabled);
            ServiceHelper.SetStartupType(RDBSS_SERVICE_NAME, ServiceStartMode.Manual);
            ServiceHelper.SetStartupType(SRV2_SERVICE_NAME, ServiceStartMode.Disabled);

            _dismService.DisableFeature(SMB_DIRECT_FEATURE_NAME);

            _lanmanWorkstationConfigurationStore.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            ServiceHelper.SetStartupType(KSECPKG_SERVICE_NAME, ServiceStartMode.Boot);
            ServiceHelper.SetStartupType(LANMANSERVER_SERVICE_NAME, ServiceStartMode.Automatic);
            ServiceHelper.SetStartupType(LANMANWORKSTATION_SERVICE_NAME, ServiceStartMode.Automatic);
            ServiceHelper.SetStartupType(MRXSMB_SERVICE_NAME, ServiceStartMode.Manual);
            ServiceHelper.SetStartupType(MRXSMB20_SERVICE_NAME, ServiceStartMode.Manual);
            ServiceHelper.SetStartupType(RDBSS_SERVICE_NAME, ServiceStartMode.System);
            ServiceHelper.SetStartupType(SRV2_SERVICE_NAME, ServiceStartMode.Manual);

            _dismService.EnableFeature(SMB_DIRECT_FEATURE_NAME);

            _lanmanWorkstationConfigurationStore.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            bool[] checks =
            {
                ServiceHelper.IsStartupTypeMatch(KSECPKG_SERVICE_NAME, ServiceStartMode.Boot),
                ServiceHelper.IsStartupTypeMatch(LANMANSERVER_SERVICE_NAME, ServiceStartMode.Automatic),
                ServiceHelper.IsStartupTypeMatch(LANMANWORKSTATION_SERVICE_NAME, ServiceStartMode.Automatic),
                ServiceHelper.IsStartupTypeMatch(MRXSMB_SERVICE_NAME, ServiceStartMode.Manual),
                ServiceHelper.IsStartupTypeMatch(MRXSMB20_SERVICE_NAME, ServiceStartMode.Manual),
                ServiceHelper.IsStartupTypeMatch(RDBSS_SERVICE_NAME, ServiceStartMode.System),
                ServiceHelper.IsStartupTypeMatch(SRV2_SERVICE_NAME, ServiceStartMode.Manual),
                _dismService.GetFeatureState(SMB_DIRECT_FEATURE_NAME) is DismPackageFeatureState.Staged or DismPackageFeatureState.Installed
            };

            return checks.All(x => x);
        }
    }
}
