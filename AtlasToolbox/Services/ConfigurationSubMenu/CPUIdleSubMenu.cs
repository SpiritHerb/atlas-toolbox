using AtlasToolbox.Stores;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Services.ConfigurationSubMenu
{
    public class CPUIdleSubMenu : IConfigurationSubMenu
    {
        private readonly ConfigurationStoreSubMenu _cpuIdleSubMenu;

        public CPUIdleSubMenu(
            [FromKeyedServices("CPUIdleSubMenu")] ConfigurationStoreSubMenu cpuIdleSubMenu)
        {
            _cpuIdleSubMenu = cpuIdleSubMenu;
        }

        public void AddConfigurationService()
        {
            throw new NotImplementedException();
        }
    }
}
