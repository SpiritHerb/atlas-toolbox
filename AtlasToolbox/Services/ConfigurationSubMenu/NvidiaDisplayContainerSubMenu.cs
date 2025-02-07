using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationSubMenu
{
    public class NvidiaDisplayContainerSubMenu : IConfigurationSubMenu
    {
        private readonly ConfigurationStoreSubMenu _nvidiaDisplayContainerSubMenu;
        public NvidiaDisplayContainerSubMenu(
            [FromKeyedServices("NvidiaDisplayContainerSubMenu")] ConfigurationStoreSubMenu nvidiaDisplayContainerSubMenu)
        {
            _nvidiaDisplayContainerSubMenu = nvidiaDisplayContainerSubMenu;
        }
    }
}