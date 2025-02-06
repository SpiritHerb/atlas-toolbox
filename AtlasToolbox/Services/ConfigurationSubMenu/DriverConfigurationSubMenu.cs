using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationSubMenu
{
    public class DriverConfigurationSubMenu : IConfigurationSubMenu
    {
        private readonly ConfigurationStoreSubMenu _driverConfigurationSubMenu;

        public DriverConfigurationSubMenu(
            [FromKeyedServices("DriverConfigurationSubMenu")] ConfigurationStoreSubMenu driverConfigurationSubMenu)
        {
            _driverConfigurationSubMenu = driverConfigurationSubMenu;
        }
    }
}
