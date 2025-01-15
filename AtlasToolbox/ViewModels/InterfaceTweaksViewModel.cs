using AtlasToolbox.Enums;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AtlasToolbox.ViewModels
{
    class InterfaceTweaksViewModel
    {
        private IEnumerable<ConfigurationItemViewModel> ConfigurationItemViewModels { get; }

        private IEnumerable<ConfigurationSubMenuViewModel> ConfigurationSubMenuViewModels { get; }

        private IEnumerable<MultiOptionConfigurationItemViewModel> MultiOptionConfigurationItemViewModels { get; }


        public ObservableCollection<ConfigurationItemViewModel> ConfigurationItem { get; set; }
        public ObservableCollection<ConfigurationSubMenuViewModel> ConfigurationItemSubMenu { get; set; }
        public ObservableCollection<MultiOptionConfigurationItemViewModel> MultiOptionConfigurationItem { get; set; }

        public InterfaceTweaksViewModel(
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
                if (configurationItem.Type == ConfigurationType.Interface)
                {
                    ConfigurationItem.Add(configurationItem);
                }
            }
            foreach (ConfigurationSubMenuViewModel configurationSubMenuItem in ConfigurationSubMenuViewModels)
            {
                if (configurationSubMenuItem.Type == ConfigurationType.Interface)
                {
                    ConfigurationItemSubMenu.Add(configurationSubMenuItem);
                }
            }
            foreach (MultiOptionConfigurationItemViewModel multiOptionConfigurationItemViewModel in MultiOptionConfigurationItemViewModels)
            {
                if (multiOptionConfigurationItemViewModel.Type == ConfigurationType.Interface)
                {
                    MultiOptionConfigurationItem.Add(multiOptionConfigurationItemViewModel);
                }
            }

            MultiOptionConfigurationItemViewModels = multiOptionConfigurationItemViewModels;
        }

        public static InterfaceTweaksViewModel LoadViewModel(
            IEnumerable<ConfigurationItemViewModel> configurationItemViewModels,
            IEnumerable<MultiOptionConfigurationItemViewModel> multiOptionConfigurationItemViewModels,
            IEnumerable<ConfigurationSubMenuViewModel> configurationSubMenuViewModels)
        {
            InterfaceTweaksViewModel viewModel = new(configurationItemViewModels, configurationSubMenuViewModels, multiOptionConfigurationItemViewModels);

            return viewModel;
        }
    }
}
