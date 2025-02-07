using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Stores;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace AtlasToolbox.Services.ConfigurationSubMenu
{
    public class AiSubMenu : IConfigurationSubMenu
    {

        private readonly ConfigurationStoreSubMenu _aiSubMenu;

        public AiSubMenu(
            [FromKeyedServices("AiSubMenu")] ConfigurationStoreSubMenu aiSubMenu)
        {
            _aiSubMenu = aiSubMenu;
        }
    }
}
