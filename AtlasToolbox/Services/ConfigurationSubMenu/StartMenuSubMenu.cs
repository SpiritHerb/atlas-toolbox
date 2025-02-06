using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationSubMenu
{
    public class StartMenuSubMenu : IConfigurationSubMenu
    {

        private readonly ConfigurationStoreSubMenu _startMenuSubMenu;

        public StartMenuSubMenu(
            [FromKeyedServices("StartMenuSubMenu")] ConfigurationStoreSubMenu startMenuSubMenu)
        {
            _startMenuSubMenu = startMenuSubMenu;
        }

        public void AddConfigurationService()
        {

        }
    }
}
