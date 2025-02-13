using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationSubMenu
{
    public class FileSharingSubMenu : IConfigurationSubMenu
    {
        private readonly ConfigurationStoreSubMenu _configurationStoreSubMenu;
        public FileSharingSubMenu(
            [FromKeyedServices("FileSharingSubMenu")] ConfigurationStoreSubMenu configurationStoreSubMenu)
        {
            _configurationStoreSubMenu = configurationStoreSubMenu;
        }
    }
}
