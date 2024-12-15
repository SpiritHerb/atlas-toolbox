using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Stores;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.ViewModels
{
    public class ConfigurationItemMenuViewModel
    {
        private readonly IConfigurationMenu _configurationMenu;
        private readonly IEnumerable<ConfigurationItemViewModel> _configurationItems;

        public ConfigurationItemMenuViewModel(
            IConfigurationMenu configurationMenu, 
            IEnumerable<ConfigurationItemViewModel> configurationItems)
        {
            _configurationItems = configurationItems;
            _configurationMenu = configurationMenu;
        }

    }
}
