using AtlasToolbox.Enums;
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
using Microsoft.UI.Xaml.Media.Animation;
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
        private object configType;
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

                // Gets all the configuration services
                ItemsControl.ItemsSource = item.ConfigurationItems;
                MultiOptionItemsControl.ItemsSource = item.MultiOptionConfigurationItems;
                Links.ItemsSource = item.LinksViewModels;
                SubMenuItems.ItemsSource = item.ConfigurationSubMenuViewModels;
                ConfigurationButton.ItemsSource = item.ConfigurationButtonViewModels;
                TitleTxt.Text += item.Name;
            }
        }

        private void OnCardClicked(object sender, RoutedEventArgs e)
        {
            var settingCard = sender as SettingsCard;
            var item = settingCard.DataContext as ConfigurationSubMenuViewModel;

            var template = SubMenuItems.ItemTemplate;

            Frame.Navigate(typeof(SubSection), new Tuple<ConfigurationSubMenuViewModel, DataTemplate>(item, template), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
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
