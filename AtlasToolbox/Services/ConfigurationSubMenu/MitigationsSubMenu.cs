using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationSubMenu
{
    public class MitigationsSubMenu : IConfigurationSubMenu
    {
        private readonly ConfigurationStoreSubMenu _mitigationsSubMenu;

        public MitigationsSubMenu(
            [FromKeyedServices("MitigationsSubMenu")] ConfigurationStoreSubMenu mitigationsSubMenu)
        {
            _mitigationsSubMenu = mitigationsSubMenu;
        }
    }
}
