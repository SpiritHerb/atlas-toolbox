using AtlasToolbox.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using ICSharpCode.Decompiler.TypeSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AtlasToolbox.ViewModels
{
    class GeneralConfigViewModel : ObservableObject
    {
        //private IEnumerable<ConfigurationItemViewModel> ConfigurationItemViewModels { get; }
        //private IEnumerable<MultiOptionConfigurationItemViewModel> MultiOptionConfigurationItemViewModels { get; }
        //private IEnumerable<ConfigurationSubMenuViewModel> ConfigurationSubMenuViewModels { get; }

        public ObservableCollection<ConfigurationItemViewModel> ConfigurationItem { get; set; }
        public ObservableCollection<MultiOptionConfigurationItemViewModel> MultiOptionConfigurationItem { get; set; }
        public ObservableCollection<ConfigurationSubMenuViewModel> ConfigurationItemSubMenu { get; set; }

        public GeneralConfigViewModel(
            IEnumerable<ConfigurationItemViewModel> configurationItemViewModels,
            IEnumerable<ConfigurationSubMenuViewModel> configurationSubMenuViewModel,
            IEnumerable<MultiOptionConfigurationItemViewModel> multiOptionConfigurationItemViewModels)
        {

            //ConfigurationItemViewModels = configurationItemViewModels;
            //MultiOptionConfigurationItemViewModels = multiOptionConfigurationItemViewModels;
            //ConfigurationSubMenuViewModels = configurationSubMenuViewModel;

            ConfigurationItem = new ObservableCollection<ConfigurationItemViewModel>(configurationItemViewModels);
            MultiOptionConfigurationItem = new ObservableCollection<MultiOptionConfigurationItemViewModel>(multiOptionConfigurationItemViewModels);
            ConfigurationItemSubMenu = new ObservableCollection<ConfigurationSubMenuViewModel>(configurationSubMenuViewModel);

            //ConfigurationItem = (ObservableCollection<ConfigurationItemViewModel>)configurationItemViewModels;
            //MultiOptionConfigurationItem = (ObservableCollection<MultiOptionConfigurationItemViewModel>)multiOptionConfigurationItemViewModels;
            //ConfigurationItemSubMenu = (ObservableCollection<ConfigurationSubMenuViewModel>)configurationSubMenuViewModel;

            //foreach (ConfigurationItemViewModel configurationItem in ConfigurationItemViewModels)
            //{
            //    if (configurationItem.Type == ConfigurationType.General)
            //    {
            //        ConfigurationItem.Add(configurationItem);
            //    }
            //}
            //foreach (ConfigurationSubMenuViewModel configurationSubMenuItem in ConfigurationSubMenuViewModels)
            //{
            //    if (configurationSubMenuItem.Type == ConfigurationType.General)
            //    {
            //        ConfigurationItemSubMenu.Add(configurationSubMenuItem);
            //    }
            //}
            //foreach (MultiOptionConfigurationItemViewModel multiOptionConfigurationItemViewModel in MultiOptionConfigurationItemViewModels)
            //{
            //    if (multiOptionConfigurationItemViewModel.Type == ConfigurationType.General)
            //    {
            //        MultiOptionConfigurationItem.Add(multiOptionConfigurationItemViewModel);
            //    }
            //}
        }

        public void ShowForType(ConfigurationType configurationType)
        {
            //ConfigurationItem.Clear();
            //ConfigurationItemSubMenu.Clear();
            //MultiOptionConfigurationItem.Clear();

            //foreach (ConfigurationItemViewModel configurationItem in ConfigurationItemViewModels)
            //{
            //    if (configurationItem.Type == configurationType)
            //    {
            //        ConfigurationItem.Add(configurationItem);
            //    }
            //}
            //foreach (ConfigurationSubMenuViewModel configurationSubMenuItem in ConfigurationSubMenuViewModels)
            //{
            //    if (configurationSubMenuItem.Type == configurationType)
            //    {
            //        ConfigurationItemSubMenu.Add(configurationSubMenuItem);
            //    }
            //}
            //foreach (MultiOptionConfigurationItemViewModel multiOptionConfigurationItemViewModel in MultiOptionConfigurationItemViewModels)
            //{
            //    if (multiOptionConfigurationItemViewModel.Type == configurationType)
            //    {
            //        MultiOptionConfigurationItem.Add(multiOptionConfigurationItemViewModel);
            //    }
            //}
        }

        public static GeneralConfigViewModel LoadViewModel(
            IEnumerable<ConfigurationItemViewModel> configurationItemViewModels,
            IEnumerable<MultiOptionConfigurationItemViewModel> multiOptionConfigurationItemViewModels,
            IEnumerable<ConfigurationSubMenuViewModel> configurationSubMenuViewModels)
        {
            GeneralConfigViewModel viewModel = new(configurationItemViewModels, configurationSubMenuViewModels, multiOptionConfigurationItemViewModels);

            return viewModel;
        }
    }
}
