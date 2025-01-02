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
               var item = toggleSwitch.DataContext as ConfigurationItemViewModel;
               if (toggleSwitch.IsOn)
               {
                   item.CurrentSetting = true;
                   item.SaveConfigurationCommand.Execute(toggleSwitch);
               }
               else
               {
                   item.CurrentSetting = false;
                   item.SaveConfigurationCommand.Execute(toggleSwitch);
               }
           }
        }
    }
}
