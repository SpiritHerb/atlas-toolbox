using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AtlasToolbox.Enums;
using AtlasToolbox.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVMEssentials.Services;
using Microsoft.UI.Xaml.Data;

namespace AtlasToolbox.ViewModels
{
    class GeneralConfigViewModel
    {
        public IEnumerable<ConfigurationItemViewModel> ConfigurationItemViewModels { get; }

        public IEnumerable<ConfigurationSubMenuViewModel> ConfigurationSubMenuViewModel { get; }

        public List<Object> ConfigurationItems { get; }
        public List<Object> ConfigurationItemSubMenu { get; }

        public ICollectionView FilteredTestModels { get; }

        public ConfigurationType? FilterType = ConfigurationType.General;
        public GeneralConfigViewModel(
            IEnumerable<ConfigurationItemViewModel> configurationItemViewModels, 
            IEnumerable<ConfigurationSubMenuViewModel> configurationSubMenuViewModel)
        {

            ConfigurationItemViewModels = configurationItemViewModels;
            ConfigurationSubMenuViewModel = configurationSubMenuViewModel;

            ConfigurationItems = new List<object>();
            ConfigurationItemSubMenu = new List<object>();

            foreach (var configurationItem in ConfigurationItemViewModels)
            {
                if (configurationItem.Type == ConfigurationType.General)
                {
                    ConfigurationItems.Add(configurationItem);
                }
            }
            foreach (var configurationSubMenuItem in ConfigurationSubMenuViewModel)
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
