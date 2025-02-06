using System;
using System.Collections.ObjectModel;
using System.Linq;
using AtlasToolbox.Enums;
using AtlasToolbox.ViewModels;
using CommunityToolkit.WinUI.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AtlasToolbox.Views;

public sealed partial class ConfigPage : Page
{
    private readonly ConfigPageViewModel _viewModel;
    private object configType;

    public ConfigPage()
    {
        this.InitializeComponent();

        _viewModel = App._host.Services.GetRequiredService<ConfigPageViewModel>();
        Enum.TryParse(new ConfigurationType().GetType(), App.CurrentCategory, out configType);
        _viewModel.ShowForType((ConfigurationType)configType);
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

    private async void LinkCard_Click(object sender, RoutedEventArgs e)
    {
        var linkCard = sender as SettingsCard;
        var linkVM = linkCard.DataContext as LinksViewModel;
        await Windows.System.Launcher.LaunchUriAsync(new Uri(linkVM.Link));
    }
}
