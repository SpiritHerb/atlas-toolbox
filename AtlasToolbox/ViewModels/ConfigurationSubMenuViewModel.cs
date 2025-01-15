using AtlasToolbox.Enums;
using AtlasToolbox.Models;
using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Services.ConfigurationSubMenu;
using AtlasToolbox.Stores;
using System.Collections.ObjectModel;

namespace AtlasToolbox.ViewModels
{
    public class ConfigurationSubMenuViewModel
    {
        private readonly ConfigurationStoreSubMenu _configurationStoreSubMenu;

        public ObservableCollection<ConfigurationItemViewModel> ConfigurationItems { get; set; }
        public ObservableCollection<MultiOptionConfigurationItemViewModel> MultiOptionConfigurationItems { get; set; }

        public ConfigurationSubMenu _configurationSubMenu { get; set; }
        public string Name => _configurationSubMenu.Name;
        public string Description => _configurationSubMenu.Description;
        public ConfigurationType Type => _configurationSubMenu.Type;

        public ConfigurationSubMenuViewModel() { }

        public ConfigurationSubMenuViewModel(
            ConfigurationSubMenu configurationSubMenu,
            ConfigurationStoreSubMenu configurationStoreSubMenu,
            ObservableCollection<ConfigurationItemViewModel> configurationItems,
            ObservableCollection<MultiOptionConfigurationItemViewModel> multiOptionConfigurationItems)
        {
            _configurationSubMenu = configurationSubMenu;
            ConfigurationItems = configurationItems;
            _configurationStoreSubMenu = configurationStoreSubMenu;
            MultiOptionConfigurationItems = multiOptionConfigurationItems;
        }
    }
}
