using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationSubMenu
{
    public class DefenderSubMenu : IConfigurationSubMenu
    {
        private readonly ConfigurationStore _defenderSubMenu;
        public DefenderSubMenu(
            [FromKeyedServices("DefenderSubMenu")] ConfigurationStore defenderSubMenu)
        {
            _defenderSubMenu = defenderSubMenu;
        }
    }
}
