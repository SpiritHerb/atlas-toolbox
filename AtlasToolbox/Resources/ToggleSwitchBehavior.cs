using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using AtlasToolbox.ViewModels;

namespace AtlasToolbox
{
    public static class ToggleSwitchBehavior
    {
        public static readonly DependencyProperty CurrentSettingProperty =
            DependencyProperty.RegisterAttached(
                "CurrentSetting",
                typeof(bool),
                typeof(ToggleSwitchBehavior),
                new PropertyMetadata(false, OnCurrentSettingChanged));
    
        public static bool GetCurrentSetting(DependencyObject obj)
        {
            return (bool)obj.GetValue(CurrentSettingProperty);
        }
    
        public static void SetCurrentSetting(DependencyObject obj, bool value)
        {
            obj.SetValue(CurrentSettingProperty, value);
        }
    
        private static void OnCurrentSettingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ToggleSwitch toggleSwitch)
            {
                if ((bool)e.NewValue)
                {
                    toggleSwitch.Toggled += OnToggled;
                }
                else
                {
                    toggleSwitch.Toggled -= OnToggled;
                }
            }
        }
    
        private static void OnToggled(object sender, RoutedEventArgs e)
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
