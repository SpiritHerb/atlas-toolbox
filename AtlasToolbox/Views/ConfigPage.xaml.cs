using System;
using System.Collections.ObjectModel;
using System.Linq;
using AtlasToolbox.Enums;
using AtlasToolbox.ViewModels;
using CommunityToolkit.WinUI.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;

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

        ConfigurationType type = (ConfigurationType)configType;
        TitleTxt.Text += type.GetDescription();
    }
    private void OnCardClicked(object sender, RoutedEventArgs e)
    {
        SettingsCard settingCard = sender as SettingsCard;
        ConfigurationSubMenuViewModel item = settingCard.DataContext as ConfigurationSubMenuViewModel;

        DataTemplate template = SubMenuItems.ItemTemplate;

        Frame.Navigate(typeof(SubSection), new Tuple<ConfigurationSubMenuViewModel, DataTemplate>(item, template), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
    }

    private void ToggleSwitch_Loaded(object sender, RoutedEventArgs e)
    {
        ToggleSwitch toggleSwitch = sender as ToggleSwitch;
        toggleSwitch.Toggled += ToggleSwitchBehavior.OnToggled;
    }

    private async void LinkCard_Click(object sender, RoutedEventArgs e)
    {
        SettingsCard linkCard = sender as SettingsCard;
        LinksViewModel linkVM = linkCard.DataContext as LinksViewModel;
        await Windows.System.Launcher.LaunchUriAsync(new Uri(linkVM.Link));
    }
}
