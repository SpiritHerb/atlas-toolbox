using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Stores;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace AtlasToolbox.Services.ConfigurationSubMenu
{
    internal class ServicesSubMenu : IConfigurationSubMenu
    {

        private readonly ConfigurationStoreSubMenu _servicesSubMenu;

        public ServicesSubMenu(
            [FromKeyedServices("ServicesSubMenu")] ConfigurationStoreSubMenu servicesSubMenu)
        {
            _servicesSubMenu = servicesSubMenu;
        }
    }
}
