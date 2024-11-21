using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using CommunityToolkit.WinUI.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AtlasToolbox.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class GeneralConfig : Page
{
    public GeneralConfig()
    {
        this.InitializeComponent();
    }

    private void toggleCopilot(object sender, RoutedEventArgs e)
    {
        if (sender is ToggleSwitch b) 
        {
            if (b.IsOn) {
                Serialization.Serialize(1);
                control1.Text = "test";
            }
            else
            {
                Serialization.Serialize(0);
                control1.Text = "womp womp";
            }
        }
    }
}
