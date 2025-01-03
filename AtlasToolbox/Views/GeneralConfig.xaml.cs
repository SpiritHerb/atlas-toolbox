using Microsoft.UI.Xaml.Controls;
using AtlasToolbox.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.UI.Xaml;
using CommunityToolkit.WinUI.Controls;
using System.Runtime.CompilerServices;
using AtlasToolbox.Enums;
using Microsoft.UI.Xaml.Media;
using ICSharpCode.Decompiler.IL.ControlFlow;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AtlasToolbox.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class GeneralConfig : Page
{
    private readonly GeneralConfigViewModel _viewModel;

    public GeneralConfig()
    {
        this.InitializeComponent();
        _viewModel = App._host.Services.GetRequiredService<GeneralConfigViewModel>();
        this.DataContext = _viewModel;

    }

    private void OnCardClicked(object sender, RoutedEventArgs e)
    {
        var settingCard = sender as SettingsCard; 
        var item = settingCard.DataContext as ConfigurationSubMenuViewModel;

        var template = ItemsControl.ItemTemplate;

        Frame.Navigate(typeof(SubSection), new Tuple<ConfigurationSubMenuViewModel, DataTemplate>(item, template));
    }

    private void ToggleSwitch_Loaded(object sender, RoutedEventArgs e)
    {
        var toggleSwitch = sender as ToggleSwitch;
        toggleSwitch.Toggled += ToggleSwitchBehavior.OnToggled;
    }

    //private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
    //{

    //    var ConfigSwitch = sender as ToggleSwitch;
    //    var vm = ConfigSwitch.DataContext as ConfigurationItemViewModel;

    //    ConfigSwitch.IsOn = vm.CurrentSetting;
    //    ConfigSwitch.Toggled += ToggleSwitchBehavior.OnToggled;
    //}


    //public SolidColorBrush SelectColor(RiskRating value)
    //{
    //    switch (value) 
    //    { 
    //        case RiskRating.HighRisk: 
    //            return new SolidColorBrush(Microsoft.UI.Colors.Red); 
    //        case RiskRating.MediumRisk: 
    //            return new SolidColorBrush(Microsoft.UI.Colors.Green);
    //        case RiskRating.LowRisk:
    //            return new SolidColorBrush(Microsoft.UI.Colors.Blue); 
    //        default: 
    //            return new SolidColorBrush(Microsoft.UI.Colors.Gray); 
    //    }
    //}
}
