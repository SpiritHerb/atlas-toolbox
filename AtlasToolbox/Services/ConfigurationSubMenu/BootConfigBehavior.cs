using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationSubMenu
{
    internal class BootConfigBehavior : IConfigurationSubMenu
    {
        private readonly ConfigurationStoreSubMenu _bootConfigBehavior;


        public BootConfigBehavior(
            [FromKeyedServices("BootConfigBehavior")] ConfigurationStoreSubMenu bootConfigBehavior)
        {
            _bootConfigBehavior = bootConfigBehavior;
        }

        public void AddConfigurationService()
        {
            throw new NotImplementedException();
        }
    }
}
