using AtlasToolbox.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AtlasToolbox.ViewModels
{
    class GeneralConfigViewModel : ObservableObject
    {
        private IEnumerable<ConfigurationItemViewModel> ConfigurationItemViewModels { get; }

        private IEnumerable<ConfigurationSubMenuViewModel> ConfigurationSubMenuViewModels { get; }


        public ObservableCollection<ConfigurationItemViewModel> ConfigurationItem { get; set; }

        public ObservableCollection<ConfigurationSubMenuViewModel> ConfigurationItemSubMenu { get; set; }

        public GeneralConfigViewModel(
            IEnumerable<ConfigurationItemViewModel> configurationItemViewModels,
            IEnumerable<ConfigurationSubMenuViewModel> configurationSubMenuViewModel)
        {

            ConfigurationItemViewModels = configurationItemViewModels;
            ConfigurationSubMenuViewModels = configurationSubMenuViewModel;

            ConfigurationItem = new ObservableCollection<ConfigurationItemViewModel>();
            ConfigurationItemSubMenu = new ObservableCollection<ConfigurationSubMenuViewModel>();

            foreach (ConfigurationItemViewModel configurationItem in ConfigurationItemViewModels)
            {
                if (configurationItem.Type == ConfigurationType.General)
                {
                    ConfigurationItem.Add(configurationItem);
                }
            }
            foreach (ConfigurationSubMenuViewModel configurationSubMenuItem in ConfigurationSubMenuViewModels)
            {
                if (configurationSubMenuItem.Type == ConfigurationType.General)
                {
                    ConfigurationItemSubMenu.Add(configurationSubMenuItem);
                }
            }
        }

        public static GeneralConfigViewModel LoadViewModel(
            IEnumerable<ConfigurationItemViewModel> configurationItemViewModels,
            IEnumerable<ConfigurationSubMenuViewModel> configurationSubMenuViewModels)
        {
            GeneralConfigViewModel viewModel = new(configurationItemViewModels, configurationSubMenuViewModels);

            return viewModel;
        }
    }
}
