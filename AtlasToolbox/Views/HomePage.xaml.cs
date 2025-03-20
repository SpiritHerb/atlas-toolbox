using AtlasToolbox.Models;
using AtlasToolbox.Utils;
using AtlasToolbox.ViewModels;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Principal;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.AtlasToolboxVtableClasses;

namespace AtlasToolbox.Views
{
    public sealed partial class HomePage : Page
    {
        private HomePageViewModel _viewModel;

        public HomePage()
        {
            this.InitializeComponent();
            _viewModel = App._host.Services.GetRequiredService<HomePageViewModel>();
            this.DataContext = _viewModel;

            ProfilesListView.ItemsSource = _viewModel.ProfilesList;
            ProfilesListView.SelectedItem = _viewModel.ProfileSelected;
        }
        private void buttonclick(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.m_window as MainWindow;

            mainWindow.GoToSoftwarePage();
        }

        private void AddProfile(object sender, RoutedEventArgs e)
        {
            _viewModel.AddProfileCommand.Execute(null);
            ProfileNameTextBox.Text = "";
        }

        /// <summary>
        /// Deletes the profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DeleteProfile(object sender, RoutedEventArgs e)
        {
            if (ProfilesListView.SelectedItem != null)
            {
                var selectedItem = ProfilesListView.SelectedItem as Profiles;

                if (selectedItem.Key != "default.txt")
                {
                    ContentDialog dialog = new ContentDialog();

                    dialog.XamlRoot = this.XamlRoot;
                    dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
                    dialog.Title = "Do you really wish to delete this profile?";
                    dialog.PrimaryButtonText = "Yes";
                    dialog.CloseButtonText = "Cancel";
                    dialog.DefaultButton = ContentDialogButton.Primary;
                    dialog.PrimaryButtonCommand = _viewModel.RemoveProfileCommand;

                    var result = await dialog.ShowAsync();
                }
                else
                {
                    ContentDialog dialog = new ContentDialog();

                    dialog.XamlRoot = this.XamlRoot;
                    dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
                    dialog.Title = "You cannot delete the default profile.";
                    dialog.CloseButtonText = "Ok";
                    dialog.DefaultButton = ContentDialogButton.Primary;

                    var result = await dialog.ShowAsync();
                }
            }
        }


        private async void SetProfile_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();

            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Do you really wish to set this profile?";
            dialog.PrimaryButtonText = "Yes";
            dialog.CloseButtonText = "No";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.PrimaryButtonCommand = _viewModel.SetProfileCommand;

            var result = await dialog.ShowAsync();
            RestartPCPrompt();
        }

        /// <summary>
        /// Prompts the user to restart their PC
        /// </summary>
        private async void RestartPCPrompt()
        {
            ContentDialog dialog = new ContentDialog();

            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "To fully apply the changes, please restart your PC";
            dialog.PrimaryButtonText = "Restart";
            dialog.CloseButtonText = "Later";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.PrimaryButtonCommand = new RelayCommand(ComputerStateHelper.RestartComputer);

            var result = await dialog.ShowAsync();
        }

        private void ProfileNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.Name = ProfileNameTextBox.Text;
        }
    }
}
