using AtlasToolbox.Stores;
using BcdSharp.Constants;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class NewBootMenuConfigurationService : IConfigurationService
    {
        private readonly ConfigurationStore _newBootMenuConfigurationStore;
        private readonly IBcdService _bcdService;

        public NewBootMenuConfigurationService(
            [FromKeyedServices("NewBootMenu")] ConfigurationStore newBootMenuConfigurationStore,
            IBcdService bcdService)
        {
            _newBootMenuConfigurationStore = newBootMenuConfigurationStore;
            _bcdService = bcdService;
        }

        public void Disable()
        {
            _bcdService.SetIntegerElement(WellKnownObjectIdentifiers.Default, WellKnownElementTypes.BootMenuPolicyWinload, 0UL);

            _newBootMenuConfigurationStore.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            _bcdService.SetIntegerElement(WellKnownObjectIdentifiers.Default, WellKnownElementTypes.BootMenuPolicyWinload, 1UL);

            _newBootMenuConfigurationStore.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            object value = _bcdService.GetElementValue(WellKnownObjectIdentifiers.Default, WellKnownElementTypes.BootMenuPolicyWinload);

            return value is 1UL;
        }
    }
}
