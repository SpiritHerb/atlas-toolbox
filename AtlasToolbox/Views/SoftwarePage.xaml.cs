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
using System.Threading.Tasks;
using AtlasToolbox.Utils;

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

        private async void  Button_Click(object sender, RoutedEventArgs e)
        {
            //CurrentlyInstalling.Text += _viewModel.SelectedSoftwareItemViewModels[0];

            //Task.Run(() => _viewModel.InstallSoftware());
            //var softwarePage = App.m_window.Content as SoftwarePage;

            int percentageCount = 100 / _viewModel.SelectedSoftwareItemViewModels.Count;

            ProgressRingStackPanel.Visibility = Visibility.Visible;
            foreach (SoftwareItemViewModel package in _viewModel.SelectedSoftwareItemViewModels)
            {
                DownloadingProgressBar.Value += percentageCount;
                CurrentlyInstalling.Text = $"Currently Installing : {package.Name}";
                await Task.Run(() => CommandPromptHelper.RunCommand($"winget install -e --id {package.Key} --accept-package-agreements --accept-source-agreements --disable-interactivity --force -h"));
            }
            ProgressRingStackPanel.Visibility = Visibility.Collapsed;
            _viewModel.SelectedSoftwareItemViewModels.Clear();
        }
    }
}
