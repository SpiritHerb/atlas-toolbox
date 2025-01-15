using AtlasToolbox.Models;
using AtlasToolbox.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AtlasToolbox.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SubSection : Page
    {
        public SubSection()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is Tuple<ConfigurationSubMenuViewModel, DataTemplate> parameter)
            {
                var item = parameter.Item1;

                ObservableCollection<ConfigurationItemViewModel> configurationItemViewModels = new ObservableCollection<ConfigurationItemViewModel>();
                ObservableCollection<MultiOptionConfigurationItemViewModel> multiOptionConfigurationItemViewModels = new ObservableCollection<MultiOptionConfigurationItemViewModel>();
                foreach (ConfigurationItemViewModel configurationItemViewModel in item.ConfigurationItems) 
                {
                    configurationItemViewModels.Add(configurationItemViewModel);
                }
                foreach (MultiOptionConfigurationItemViewModel configurationItemViewModel in item.MultiOptionConfigurationItems)
                {
                    multiOptionConfigurationItemViewModels.Add(configurationItemViewModel);
                }

                MultiOptionItemsControl.ItemsSource = multiOptionConfigurationItemViewModels;
                ItemsControl.ItemsSource = configurationItemViewModels;
            }
            
        }
        private void ToggleSwitch_Loaded(object sender, RoutedEventArgs e)
        {
            var toggleSwitch = sender as ToggleSwitch;
            toggleSwitch.Toggled += ToggleSwitchBehavior.OnToggled;
        }
    }
}
