using AtlasToolbox.ViewModels;
using CommunityToolkit.WinUI.Controls;
using Microsoft.Extensions.DependencyInjection;
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

namespace AtlasToolbox.Views
{
    public sealed partial class Troubleshooting : Page
    {
        private readonly ConfigPageViewModel _viewModel;
        public Troubleshooting()
        {
            this.InitializeComponent();
            _viewModel = App._host.Services.GetRequiredService<ConfigPageViewModel>();
            _viewModel.ShowForType(Enums.ConfigurationType.Troubleshooting);
            this.DataContext = _viewModel;
        }
        private void OnCardClicked(object sender, RoutedEventArgs e)
        {
            var settingCard = sender as SettingsCard;
            var item = settingCard.DataContext as ConfigurationSubMenuViewModel;

            var template = SubMenuItems.ItemTemplate;

            Frame.Navigate(typeof(SubSection), new Tuple<ConfigurationSubMenuViewModel, DataTemplate>(item, template));
        }

        private void ToggleSwitch_Loaded(object sender, RoutedEventArgs e)
        {
            var toggleSwitch = sender as ToggleSwitch;
            toggleSwitch.Toggled += ToggleSwitchBehavior.OnToggled;
        }
    }
}