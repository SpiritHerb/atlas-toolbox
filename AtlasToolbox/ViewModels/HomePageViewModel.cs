using ABI.System.Collections;
using AtlasToolbox.Models;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinRT.AtlasToolboxVtableClasses;

namespace AtlasToolbox.ViewModels
{
    public partial class HomePageViewModel
    {
        private ObservableCollection<Profiles> profiles;

        public List<Profiles> _profiles;

        public string _name;

        public Profiles profileSelected;

        public HomePageViewModel() 
        {
            _profiles = profiles.ToList();
        }

        [RelayCommand]
        private void AddProfile()
        {

        }
    }
}
