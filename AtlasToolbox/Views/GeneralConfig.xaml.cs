using System;
using System.Collections.ObjectModel;
using System.Linq;
using AtlasToolbox.ViewModels;
using CommunityToolkit.WinUI.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AtlasToolbox.Views;

public sealed partial class GeneralConfig : Page
{
    private readonly ConfigPageViewModel _viewModel;
    public GeneralConfig()
    {
        this.InitializeComponent();
        _viewModel = App._host.Services.GetRequiredService<ConfigPageViewModel>();
        _viewModel.ShowForType(Enums.ConfigurationType.General);
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
