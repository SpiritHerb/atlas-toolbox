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

namespace AtlasToolbox.ViewModels
{
    class GeneralConfigViewModel
    {
        public IEnumerable<ConfigurationItemViewModel> ConfigurationItemViewModels { get; }

        public IEnumerable<ConfigurationSubMenuViewModel> ConfigurationSubMenuViewModel { get; }

        public ICommand FilterCommand { get; }

        public ConfigurationType? FilterType = ConfigurationType.General;
        public GeneralConfigViewModel(
            IEnumerable<ConfigurationItemViewModel> configurationItemViewModels, 
            IEnumerable<ConfigurationSubMenuViewModel> configurationSubMenuViewModel)
        {

            ConfigurationItemViewModels = configurationItemViewModels;
            ConfigurationSubMenuViewModel = configurationSubMenuViewModel;            
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
