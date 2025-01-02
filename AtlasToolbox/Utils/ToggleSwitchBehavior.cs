using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using AtlasToolbox.ViewModels;

namespace AtlasToolbox
{
    public static class ToggleSwitchBehavior
    {
        //public static readonly DependencyProperty CurrentSettingProperty =
        //    DependencyProperty.RegisterAttached(
        //        "CurrentSetting",
        //        typeof(bool),
        //        typeof(ToggleSwitchBehavior),
        //        new PropertyMetadata(false, OnCurrentSettingChanged));

        //private static readonly DependencyProperty IsInitializedProperty =
        //    DependencyProperty.RegisterAttached(
        //        "IsInitialized",
        //        typeof(bool),
        //        typeof(ToggleSwitchBehavior),
        //        new PropertyMetadata(false));

        //public static bool GetCurrentSetting(DependencyObject obj)
        //{
        //    return (bool)obj.GetValue(CurrentSettingProperty);
        //}

        //public static void SetCurrentSetting(DependencyObject obj, bool value)
        //{
        //    obj.SetValue(CurrentSettingProperty, value);
        //}

        //private static bool GetIsInitialized(DependencyObject obj)
        //{
        //    return (bool)obj.GetValue(IsInitializedProperty);
        //}

        //private static void SetIsInitialized(DependencyObject obj, bool value)
        //{
        //    obj.SetValue(IsInitializedProperty, value);
        //}

        //private static void OnCurrentSettingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    if (d is ToggleSwitch toggleSwitch)
        //    {
        //        toggleSwitch.Toggled += OnToggled;
        //        SetIsInitialized(toggleSwitch, true);
        //    }
        //}

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
