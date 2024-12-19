using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;
using System.ServiceProcess;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class SearchIndexingConfigurationService : IConfigurationService
    {
        private const string WSEARCH_SERVICE_NAME = "WSearch";

        private readonly ConfigurationStore _searchIndexingConfigurationStore;

        public SearchIndexingConfigurationService(
            [FromKeyedServices("SearchIndexing")] ConfigurationStore searchIndexingConfigurationStore)
        {
            _searchIndexingConfigurationStore = searchIndexingConfigurationStore;
        }

        public void Disable()
        {
            ServiceHelper.SetStartupType(WSEARCH_SERVICE_NAME, ServiceStartMode.Disabled);

            _searchIndexingConfigurationStore.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            ServiceHelper.SetStartupType(WSEARCH_SERVICE_NAME, ServiceStartMode.Automatic);

            _searchIndexingConfigurationStore.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return ServiceHelper.IsStartupTypeMatch(WSEARCH_SERVICE_NAME, ServiceStartMode.Automatic);
        }
    }
}
