using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using AtlasToolbox.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AtlasToolbox.Views
{
    public sealed partial class NewProfilePage : Page
    {
        private static HomePageViewModel _viewModel;
        public NewProfilePage(HomePageViewModel homePageViewModel)
        {
            this.InitializeComponent();
            LoadText();
            this.DataContext = homePageViewModel;
            _viewModel = homePageViewModel;
        }

        private void LoadText()
        {
            CurrentConfigSavedMessage.Text = App.GetValueFromItemList("CurrentConfigIsSavedMessage");
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.Name = ProfileName.Text;
        }
    }
}
