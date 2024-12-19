using AtlasToolbox.Stores;
using BcdSharp.Constants;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class SpinningAnimationConfigurationService : IConfigurationService
    {
        private readonly ConfigurationStore _spinningAnimationConfigurationStore;
        private readonly IBcdService _bcdService;

        public SpinningAnimationConfigurationService(
            [FromKeyedServices("SpinningAnimation")] ConfigurationStore spinningAnimationConfigurationStore,
            IBcdService bcdService)
        {
            _spinningAnimationConfigurationStore = spinningAnimationConfigurationStore;
            _bcdService = bcdService;
        }

        public void Disable()
        {
            _bcdService.SetBooleanElement(WellKnownObjectIdentifiers.GlobalSettings, WellKnownElementTypes.NoBootUxProgress, true);

            _spinningAnimationConfigurationStore.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            _bcdService.DeleteElement(WellKnownObjectIdentifiers.GlobalSettings, WellKnownElementTypes.NoBootUxProgress);

            _spinningAnimationConfigurationStore.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            object value = _bcdService.GetElementValue(WellKnownObjectIdentifiers.GlobalSettings, WellKnownElementTypes.NoBootUxProgress);

            return value is null or false;
        }
    }
}
