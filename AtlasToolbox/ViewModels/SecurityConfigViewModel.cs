using AtlasToolbox.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.ViewModels
{
    public class SecurityConfigViewModel
    {
        private IEnumerable<ConfigurationItemViewModel> ConfigurationItemViewModels { get; }

        private IEnumerable<ConfigurationSubMenuViewModel> ConfigurationSubMenuViewModels { get; }

        private IEnumerable<MultiOptionConfigurationItemViewModel> MultiOptionConfigurationItemViewModels { get; }


        public ObservableCollection<ConfigurationItemViewModel> ConfigurationItem { get; set; }
        public ObservableCollection<ConfigurationSubMenuViewModel> ConfigurationItemSubMenu { get; set; }
        public ObservableCollection<MultiOptionConfigurationItemViewModel> MultiOptionConfigurationItem { get; set; }

        public SecurityConfigViewModel(
            IEnumerable<ConfigurationItemViewModel> configurationItemViewModels,
            IEnumerable<ConfigurationSubMenuViewModel> configurationSubMenuViewModel,
            IEnumerable<MultiOptionConfigurationItemViewModel> multiOptionConfigurationItemViewModels)
        {

            ConfigurationItemViewModels = configurationItemViewModels;
            ConfigurationSubMenuViewModels = configurationSubMenuViewModel;
            MultiOptionConfigurationItemViewModels = multiOptionConfigurationItemViewModels;

            ConfigurationItem = new ObservableCollection<ConfigurationItemViewModel>();
            ConfigurationItemSubMenu = new ObservableCollection<ConfigurationSubMenuViewModel>();
            MultiOptionConfigurationItem = new ObservableCollection<MultiOptionConfigurationItemViewModel>();

            foreach (ConfigurationItemViewModel configurationItem in ConfigurationItemViewModels)
            {
                if (configurationItem.Type == ConfigurationType.Security)
                {
                    ConfigurationItem.Add(configurationItem);
                }
            }
            foreach (ConfigurationSubMenuViewModel configurationSubMenuItem in ConfigurationSubMenuViewModels)
            {
                if (configurationSubMenuItem.Type == ConfigurationType.Security)
                {
                    ConfigurationItemSubMenu.Add(configurationSubMenuItem);
                }
            }
            foreach (MultiOptionConfigurationItemViewModel multiOptionConfigurationItemViewModel in MultiOptionConfigurationItemViewModels)
            {
                if (multiOptionConfigurationItemViewModel.Type == ConfigurationType.Security)
                {
                    MultiOptionConfigurationItem.Add(multiOptionConfigurationItemViewModel);
                }
            }

            MultiOptionConfigurationItemViewModels = multiOptionConfigurationItemViewModels;
        }

        public static SecurityConfigViewModel LoadViewModel(
            IEnumerable<ConfigurationItemViewModel> configurationItemViewModels,
            IEnumerable<MultiOptionConfigurationItemViewModel> multiOptionConfigurationItemViewModels,
            IEnumerable<ConfigurationSubMenuViewModel> configurationSubMenuViewModels)
        {
            SecurityConfigViewModel viewModel = new(configurationItemViewModels, configurationSubMenuViewModels, multiOptionConfigurationItemViewModels);

            return viewModel;
        }
    }
}

