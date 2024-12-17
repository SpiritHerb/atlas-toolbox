using AtlasToolbox.Enums;
using AtlasToolbox.Models;
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
    public class ConfigurationSubMenuViewModel
    {
        private readonly IConfigurationSubMenu _iConfigurationSubMenu;
        private readonly IEnumerable<ConfigurationItemViewModel> _configurationItems;

        public ConfigurationSubMenu _configurationSubMenu { get; set; }
        public string Name => _configurationSubMenu.Name;
        public ConfigurationSubMenuTypes Type => _configurationSubMenu.Type;

        public ConfigurationSubMenuViewModel(
            ConfigurationSubMenu configurationSubMenu,
            IConfigurationSubMenu iConfigurationSubMenu, 
            IEnumerable<ConfigurationItemViewModel> configurationItems)
        {
            _configurationSubMenu = configurationSubMenu;
            _configurationItems = configurationItems;
            _iConfigurationSubMenu = iConfigurationSubMenu;
        }
    }
}
