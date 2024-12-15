using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVMEssentials.Services;

namespace AtlasToolbox.ViewModels
{
    class GeneralConfigViewModel
    {
        public IEnumerable<ConfigurationItemViewModel> ConfigurationItemViewModels { get; }

        public IEnumerable<ConfigurationItemMenuViewModel> ConfigurationItemMenuViewModel { get; }

        public GeneralConfigViewModel(
            IEnumerable<ConfigurationItemViewModel> configurationItemViewModels, 
            IEnumerable<ConfigurationItemMenuViewModel> configurationItemMenuViewModel)
        {
            ConfigurationItemViewModels = configurationItemViewModels;
            ConfigurationItemMenuViewModel = configurationItemMenuViewModel;
        }

        public static GeneralConfigViewModel LoadViewModel(
            IEnumerable<ConfigurationItemViewModel> configurationItemViewModels,
            IEnumerable<ConfigurationItemMenuViewModel> configurationMenuItemsViewModels)
        {
            GeneralConfigViewModel viewModel = new(configurationItemViewModels, configurationMenuItemsViewModels);

            return viewModel;
        }
    }
}
