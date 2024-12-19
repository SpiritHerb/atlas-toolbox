using AtlasToolbox.Stores;
using BcdSharp.Constants;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class HighestModeConfigurationService : IConfigurationService
    {
        private readonly ConfigurationStore _highestModeConfigurationStore;
        private readonly IBcdService _bcdService;

        public HighestModeConfigurationService(
            [FromKeyedServices("HighestMode")] ConfigurationStore highestModeConfigurationStore,
            IBcdService bcdService)
        {
            _highestModeConfigurationStore = highestModeConfigurationStore;
            _bcdService = bcdService;
        }

        public void Disable()
        {
            _bcdService.DeleteElement(WellKnownObjectIdentifiers.GlobalSettings, WellKnownElementTypes.HighestMode);

            _highestModeConfigurationStore.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            _bcdService.SetBooleanElement(WellKnownObjectIdentifiers.GlobalSettings, WellKnownElementTypes.HighestMode, true);

            _highestModeConfigurationStore.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            object value = _bcdService.GetElementValue(WellKnownObjectIdentifiers.GlobalSettings, WellKnownElementTypes.HighestMode);

            return value is true;
        }
    }
}
