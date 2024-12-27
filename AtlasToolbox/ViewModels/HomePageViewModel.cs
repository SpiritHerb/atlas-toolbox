using ABI.System.Collections;
using AtlasToolbox.Models;
using AtlasToolbox.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WinRT.AtlasToolboxVtableClasses;

namespace AtlasToolbox.ViewModels
{
    public partial class HomePageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Profiles> _profiles;

        public ObservableCollection<Profiles> ProfilesList
        {
            get => _profiles;
            set
            {
                OnPropertyChanged();
            }
        }

        public string Name;

        public Profiles profileSelected;

        // Implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public HomePageViewModel(
            ObservableCollection<Profiles> profiles)
        {
            _profiles = profiles;
        }

        public static HomePageViewModel LoadViewModel(
            ObservableCollection<Profiles> profiles)
        {
            HomePageViewModel viewModel = new(profiles);

            return viewModel;
        }

        [RelayCommand]
        private void AddProfile()
        {
            DirectoryInfo profilesDirectory = new DirectoryInfo("..\\..\\..\\..\\Profiles\\");
            FileInfo[] profileFile = profilesDirectory.GetFiles();

            using (StreamWriter outputFile = new StreamWriter(Path.Combine("..\\..\\..\\..\\Profiles\\", Name.Trim() + ".txt")))
            {
                outputFile.WriteLine(Name);

                IEnumerable<ConfigurationItemViewModel> configViewModels = App._host.Services.GetRequiredService<IEnumerable<ConfigurationItemViewModel>>();

                List<string> configItemKeys = new List<string>();    

                foreach (ConfigurationItemViewModel configItemViewModel in configViewModels)
                {
                    if (configItemViewModel.CurrentSetting) 
                    {
                        outputFile.WriteLine(configItemViewModel.Name);
                        configItemKeys.Add(configItemViewModel.Name);
                    }
                }
                ProfilesList.Add(new(Name, Name.Trim(), configItemKeys));               
            }
        }
    }
}
