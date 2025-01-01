using ABI.System.Collections;
using AtlasToolbox.Models;
using AtlasToolbox.Services.ConfigurationServices;
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
using Windows.Services.Maps;
using WinRT.AtlasToolboxVtableClasses;

namespace AtlasToolbox.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        private IEnumerable<Profiles> _profiles;
        private IEnumerable<ConfigurationItemViewModel> ConfigurationItemViewModels { get; }

        [ObservableProperty]
        public ObservableCollection<Profiles> _profilesList;

        public string Name;

        [ObservableProperty]
        public Profiles _profileSelected;

        public HomePageViewModel(
            IEnumerable<Profiles> profiles,
            IEnumerable<ConfigurationItemViewModel> configurationItemViewModels)
        {
            ConfigurationItemViewModels = configurationItemViewModels;
            _profilesList = new();
            foreach (Profiles profile in profiles) { ProfilesList.Add(profile); }
        }

        public static HomePageViewModel LoadViewModel(
            IEnumerable<Profiles> profiles,
            IEnumerable<ConfigurationItemViewModel> configurationItemViewModels)
        {
            HomePageViewModel viewModel = new(profiles, configurationItemViewModels);

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
                        outputFile.WriteLine(configItemViewModel.Key);
                        configItemKeys.Add(configItemViewModel.Key);
                    }
                }
                ProfilesList.Add(new(Name, Name.Trim(), configItemKeys));               
            }
        }

        [RelayCommand]
        private void RemoveProfile() 
        {
            DirectoryInfo profilesDirectory = new DirectoryInfo("..\\..\\..\\..\\Profiles\\");
            FileInfo[] profileFile = profilesDirectory.GetFiles();

            foreach (FileInfo file in profileFile.ToList())
            {
                if (ProfileSelected.Key + ".txt" == file.Name)
                {
                    File.Delete(file.FullName);
                    break;
                }
            }
            ProfilesList.Remove(ProfileSelected);
        }

        [RelayCommand]
        private void SetProfile()
        {
            List<ConfigurationItemViewModel> configurationItemVMs = ConfigurationItemViewModels.ToList();
            foreach (ConfigurationItemViewModel viewModel in configurationItemVMs)
            {
                if (ProfileSelected.ConfigurationServices.Contains(viewModel.Key))
                {
                    //ConfigurationItemViewModel config = App._host.Services.GetKeyedService<ConfigurationItemViewModel>(viewModel.Key);
                    viewModel.CurrentSetting = true;
                }
                else
                {
                    viewModel.CurrentSetting = false;
                }
            }
        }
    }
}
