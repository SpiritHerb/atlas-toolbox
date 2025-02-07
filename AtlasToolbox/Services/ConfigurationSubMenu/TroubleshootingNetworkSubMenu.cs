using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationSubMenu
{
    public class TroubleshootingNetworkSubMenu : IConfigurationSubMenu
    {
        private readonly ConfigurationStore _troubleshootingNetworkSubMenu;
        public TroubleshootingNetworkSubMenu(
            [FromKeyedServices("TroubleshootingNetwork")] ConfigurationStore troubleshootingNetworkSubMenu)
        {
            _troubleshootingNetworkSubMenu = troubleshootingNetworkSubMenu;
        }
    }
}
