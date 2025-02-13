using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using AtlasToolbox.ViewModels;

namespace AtlasToolbox
{
    public static class ToggleSwitchBehavior
    {
        public static void OnToggled(object sender, RoutedEventArgs e)
        {

           if (sender is ToggleSwitch toggleSwitch)
           {
               ConfigurationItemViewModel item = toggleSwitch.DataContext as ConfigurationItemViewModel;
               if (toggleSwitch.IsOn)
               {
                   item.CurrentSetting = true;
               }
               else
               {
                   item.CurrentSetting = false;
               }
           }
        }
    }
}
