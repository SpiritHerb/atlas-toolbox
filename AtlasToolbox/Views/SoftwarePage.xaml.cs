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

namespace AtlasToolbox
{
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
        
        /// <summary>
        /// Installs all the selected packages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void  Button_Click(object sender, RoutedEventArgs e)
        {
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
