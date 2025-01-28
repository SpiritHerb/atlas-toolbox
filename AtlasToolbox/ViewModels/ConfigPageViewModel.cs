using AtlasToolbox.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using ICSharpCode.Decompiler.TypeSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AtlasToolbox.ViewModels
{
    class ConfigPageViewModel : ObservableObject
    {
        private IEnumerable<ConfigurationItemViewModel> ConfigurationItemViewModels { get; }
        private IEnumerable<MultiOptionConfigurationItemViewModel> MultiOptionConfigurationItemViewModels { get; }
        private IEnumerable<ConfigurationSubMenuViewModel> ConfigurationSubMenuViewModels { get; }

        public ObservableCollection<ConfigurationItemViewModel> ConfigurationItem { get; set; }
        public ObservableCollection<MultiOptionConfigurationItemViewModel> MultiOptionConfigurationItem { get; set; }
        public ObservableCollection<ConfigurationSubMenuViewModel> ConfigurationItemSubMenu { get; set; }

        public ConfigPageViewModel(
            IEnumerable<ConfigurationItemViewModel> configurationItemViewModels,
            IEnumerable<ConfigurationSubMenuViewModel> configurationSubMenuViewModel,
            IEnumerable<MultiOptionConfigurationItemViewModel> multiOptionConfigurationItemViewModels)
        {

            ConfigurationItemViewModels = configurationItemViewModels;
            MultiOptionConfigurationItemViewModels = multiOptionConfigurationItemViewModels;
            ConfigurationSubMenuViewModels = configurationSubMenuViewModel;

            //ConfigurationItem = new ObservableCollection<ConfigurationItemViewModel>(configurationItemViewModels);
            //MultiOptionConfigurationItem = new ObservableCollection<MultiOptionConfigurationItemViewModel>(multiOptionConfigurationItemViewModels);
            //ConfigurationItemSubMenu = new ObservableCollection<ConfigurationSubMenuViewModel>(configurationSubMenuViewModel);
        }

        public void ShowForType(ConfigurationType configurationType)
        {
            ConfigurationItem = new ObservableCollection<ConfigurationItemViewModel>(ConfigurationItemViewModels.Where(item => item.Type == configurationType));
            MultiOptionConfigurationItem = new ObservableCollection<MultiOptionConfigurationItemViewModel>(MultiOptionConfigurationItemViewModels.Where(item => item.Type == configurationType));
            ConfigurationItemSubMenu = new ObservableCollection<ConfigurationSubMenuViewModel>(ConfigurationSubMenuViewModels.Where(item => item.Type == configurationType));
        }

        public static ConfigPageViewModel LoadViewModel(
            IEnumerable<ConfigurationItemViewModel> configurationItemViewModels,
            IEnumerable<MultiOptionConfigurationItemViewModel> multiOptionConfigurationItemViewModels,
            IEnumerable<ConfigurationSubMenuViewModel> configurationSubMenuViewModels)
        {
            ConfigPageViewModel viewModel = new(configurationItemViewModels, configurationSubMenuViewModels, multiOptionConfigurationItemViewModels);

            return viewModel;
        }
    }
}
