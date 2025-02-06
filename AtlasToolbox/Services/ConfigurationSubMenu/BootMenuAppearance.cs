using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationSubMenu
{
    public class BootMenuAppearance : IConfigurationSubMenu
    {
        private readonly ConfigurationStoreSubMenu _bootMenuAppearance;

        public BootMenuAppearance(
            [FromKeyedServices("BootMenuAppearance")] ConfigurationStoreSubMenu bootMenuAppearance)
        {
            _bootMenuAppearance = bootMenuAppearance;
        }
    }
}
