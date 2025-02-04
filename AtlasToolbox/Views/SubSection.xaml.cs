using AtlasToolbox.Models;
using AtlasToolbox.Utils;
using AtlasToolbox.ViewModels;
using CommunityToolkit.WinUI.Controls;
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

namespace AtlasToolbox.Views
{
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
                ObservableCollection<LinksViewModel> linksViewModels = new ObservableCollection<LinksViewModel>();
                foreach (ConfigurationItemViewModel configurationItemViewModel in item.ConfigurationItems) 
                {
                    configurationItemViewModels.Add(configurationItemViewModel);
                }
                foreach (MultiOptionConfigurationItemViewModel configurationItemViewModel in item.MultiOptionConfigurationItems)
                {
                    multiOptionConfigurationItemViewModels.Add(configurationItemViewModel);
                }
                foreach (LinksViewModel configurationItemViewModel in item.LinksViewModels)
                {
                    linksViewModels.Add(configurationItemViewModel);
                }

                MultiOptionItemsControl.ItemsSource = multiOptionConfigurationItemViewModels;
                ItemsControl.ItemsSource = configurationItemViewModels;
                Links.ItemsSource = linksViewModels;
            }
            
        }
        private void ToggleSwitch_Loaded(object sender, RoutedEventArgs e)
        {
            var toggleSwitch = sender as ToggleSwitch;
            toggleSwitch.Toggled += ToggleSwitchBehavior.OnToggled;
        }

        private async void LinkCard_Click(object sender, RoutedEventArgs e)
        {
            var linkCard = sender as SettingsCard;
            var linkVM = linkCard.DataContext as LinksViewModel;

            await Windows.System.Launcher.LaunchUriAsync(new Uri(linkVM.Link));
        }
    }
}
