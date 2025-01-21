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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AtlasToolbox.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InterfaceTweaks : Page
    {
        private readonly ConfigPageViewModel _viewModel;
        //public ObservableCollection<ConfigurationItemViewModel> ConfigurationItemView { get; set; }
        //public ObservableCollection<MultiOptionConfigurationItemViewModel> MultiOptionConfigurationItemView { get; set; }
        //public ObservableCollection<ConfigurationSubMenuViewModel> SubMenuConfigurationItemView { get; set; }
        public InterfaceTweaks()
        {
            //if (ConfigurationItemView is null)
            //{
            //    _viewModel = App._host.Services.GetRequiredService<ConfigPageViewModel>();
            //    //_viewModel.ShowForType(Enums.ConfigurationType.Interface);
            //    this.DataContext = _viewModel;

            //    ConfigurationItemView = new ObservableCollection<ConfigurationItemViewModel>(_viewModel.ConfigurationItem.Where(item => item.Type == Enums.ConfigurationType.Interface));
            //    MultiOptionConfigurationItemView = new ObservableCollection<MultiOptionConfigurationItemViewModel>(_viewModel.MultiOptionConfigurationItem.Where(item => item.Type == Enums.ConfigurationType.Interface));
            //    SubMenuConfigurationItemView = new ObservableCollection<ConfigurationSubMenuViewModel>(_viewModel.ConfigurationItemSubMenu.Where(item => item.Type == Enums.ConfigurationType.Interface));

            //}
            this.InitializeComponent();
            _viewModel = App._host.Services.GetRequiredService<ConfigPageViewModel>();
            _viewModel.ShowForType(Enums.ConfigurationType.Interface);
            this.DataContext = _viewModel;
            //SubMenuItems.ItemsSource = SubMenuConfigurationItemView;
            //MultiOptionItems.ItemsSource = MultiOptionConfigurationItemView;
            //ConfigurationItems.ItemsSource = ConfigurationItemView;
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