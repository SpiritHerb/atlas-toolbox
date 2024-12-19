using AtlasToolbox.Services;
using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Stores;
using BcdSharp.Constants;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class AdvancedBootOptionsConfigurationService : IConfigurationService
    {
        private readonly ConfigurationStore _advancedBootOptionsConfigurationStore;
        private readonly IBcdService _bcdService;

        public AdvancedBootOptionsConfigurationService(
            [FromKeyedServices("AdvancedBootOptions")] ConfigurationStore advancedBootOptionsConfigurationStore,
            IBcdService bcdService)
        {
            _advancedBootOptionsConfigurationStore = advancedBootOptionsConfigurationStore;
            _bcdService = bcdService;
        }

        public void Disable()
        {
            _bcdService.DeleteElement(WellKnownObjectIdentifiers.GlobalSettings, WellKnownElementTypes.AdvancedOptions);

            _advancedBootOptionsConfigurationStore.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            _bcdService.SetBooleanElement(WellKnownObjectIdentifiers.GlobalSettings, WellKnownElementTypes.AdvancedOptions, true);

            _advancedBootOptionsConfigurationStore.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            object value = _bcdService.GetElementValue(WellKnownObjectIdentifiers.GlobalSettings, WellKnownElementTypes.AdvancedOptions);

            return value is true;
        }
    }
}
