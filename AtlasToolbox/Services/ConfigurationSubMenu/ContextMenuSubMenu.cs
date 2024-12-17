using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Stores;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace AtlasToolbox.Services.ConfigurationSubMenu
{
    public class ContextMenuSubMenu : IConfigurationSubMenu
    {

        private readonly List<string> _configurationKeyedServices;


        private readonly ConfigurationStoreSubMenu _contextMenuConfigurationSubMenu;

        public ContextMenuSubMenu(
            [FromKeyedServices("ContextMenuSubMenu")] ConfigurationStoreSubMenu contextMenuSubMenu)
        {
            _contextMenuConfigurationSubMenu = contextMenuSubMenu;
        }


        public void AddConfigurationService()
        {
            _contextMenuConfigurationSubMenu.ConfigurationStores.Add(
                "AppStoreArchiving");
        }
    }
}
