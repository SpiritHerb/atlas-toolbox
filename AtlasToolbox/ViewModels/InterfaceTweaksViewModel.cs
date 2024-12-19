using AtlasToolbox.Enums;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AtlasToolbox.ViewModels
{
    class InterfaceTweaksViewModel
    {
        public IEnumerable<ConfigurationItemViewModel> ConfigurationItemViewModels { get; }

        public IEnumerable<ConfigurationSubMenuViewModel> ConfigurationSubMenuViewModel { get; }

        public List<Object> ConfigurationItems { get; }
        public List<Object> ConfigurationItemSubMenu { get; }

        public ICollectionView FilteredTestModels { get; }

        public ConfigurationType? FilterType = ConfigurationType.General;
        public InterfaceTweaksViewModel(
            IEnumerable<ConfigurationItemViewModel> configurationItemViewModels,
            IEnumerable<ConfigurationSubMenuViewModel> configurationSubMenuViewModel)
        {

            ConfigurationItemViewModels = configurationItemViewModels;
            ConfigurationSubMenuViewModel = configurationSubMenuViewModel;

            ConfigurationItems = new List<object>();
            ConfigurationItemSubMenu = new List<object>();

            foreach (var configurationItem in ConfigurationItemViewModels)
            {
                if (configurationItem.Type == ConfigurationType.Interface)
                {
                    ConfigurationItems.Add(configurationItem);
                }
            }
            foreach (var configurationSubMenuItem in ConfigurationSubMenuViewModel)
            {
                if (configurationSubMenuItem.Type == ConfigurationType.Interface)
                {
                    ConfigurationItemSubMenu.Add(configurationSubMenuItem);
                }
            }
        }

        public static InterfaceTweaksViewModel LoadViewModel(
            IEnumerable<ConfigurationItemViewModel> configurationItemViewModels,
            IEnumerable<ConfigurationSubMenuViewModel> configurationSubMenuViewModels)
        {
            InterfaceTweaksViewModel viewModel = new(configurationItemViewModels, configurationSubMenuViewModels);

            return viewModel;
        }
    }
}
