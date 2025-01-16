using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using AtlasToolbox.ViewModels;
using Microsoft.Extensions.DependencyInjection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AtlasToolbox
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SoftwarePage : Page
    {
        private SoftwarePageViewModel _viewModel;

        public SoftwarePage()
        {
            this.InitializeComponent();
            _viewModel = App._host.Services.GetRequiredService<SoftwarePageViewModel>();
            this.DataContext = _viewModel;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;

            _viewModel.SelectedSoftwareItemViewModels.Add((SoftwareItemViewModel)checkBox.DataContext);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;

            _viewModel.SelectedSoftwareItemViewModels.Remove((SoftwareItemViewModel)checkBox.DataContext);
        }
    }
}
