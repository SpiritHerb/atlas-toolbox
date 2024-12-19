using AtlasToolbox.Stores;
using BcdSharp.Constants;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class KernelParametersConfigurationService : IConfigurationService
    {
        private readonly ConfigurationStore _kernelParametersConfigurationStore;
        private readonly IBcdService _bcdService;

        public KernelParametersConfigurationService(
            [FromKeyedServices("KernelParameters")] ConfigurationStore kernelParametersConfigurationStore,
            IBcdService bcdService)
        {
            _kernelParametersConfigurationStore = kernelParametersConfigurationStore;
            _bcdService = bcdService;
        }

        public void Disable()
        {
            _bcdService.DeleteElement(WellKnownObjectIdentifiers.GlobalSettings, WellKnownElementTypes.OptionsEdit);

            _kernelParametersConfigurationStore.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            _bcdService.SetBooleanElement(WellKnownObjectIdentifiers.GlobalSettings, WellKnownElementTypes.OptionsEdit, true);

            _kernelParametersConfigurationStore.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            object value = _bcdService.GetElementValue(WellKnownObjectIdentifiers.GlobalSettings, WellKnownElementTypes.OptionsEdit);

            return value is true;
        }
    }
}
