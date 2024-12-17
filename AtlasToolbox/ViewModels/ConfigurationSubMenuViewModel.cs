using AtlasToolbox.Enums;
using AtlasToolbox.Models;
using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Services.ConfigurationSubMenu;
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
        private readonly ConfigurationStoreSubMenu _configurationStoreSubMenu;

        public IEnumerable<ConfigurationItemViewModel> _configurationItems { get; set; }

        public ConfigurationSubMenu _configurationSubMenu { get; set; }
        public string Name => _configurationSubMenu.Name;
        public string Description => _configurationSubMenu.Description;
        public ConfigurationType Type => _configurationSubMenu.Type;

        public ConfigurationSubMenuViewModel(
            ConfigurationSubMenu configurationSubMenu,
            ConfigurationStoreSubMenu configurationStoreSubMenu,
            IEnumerable<ConfigurationItemViewModel> configurationItems)
        {
            _configurationSubMenu = configurationSubMenu;
            _configurationItems = configurationItems;
            _configurationStoreSubMenu = configurationStoreSubMenu;
        }
    }
}
