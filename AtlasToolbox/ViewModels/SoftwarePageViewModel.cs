using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.ViewModels
{
    public class SoftwarePageViewModel
    {
        public ObservableCollection<SoftwareItemViewModel> SoftwareItemViewModels { get; set; }
        public List<SoftwareItemViewModel> SelectedSoftwareItemViewModels { get; set; }

        public SoftwarePageViewModel(IEnumerable<SoftwareItemViewModel> softwareItemViewModels)
        {
            //SoftwareItemViewModels = softwareItemViewModels;
            // TODO please change this and figure out something better this is stupid
            SelectedSoftwareItemViewModels = new List<SoftwareItemViewModel>();
            SoftwareItemViewModels = new ObservableCollection<SoftwareItemViewModel>();

            foreach (var itemViewModel in softwareItemViewModels)
            {
                SoftwareItemViewModels.Add(itemViewModel);
            }
        }

        public static SoftwarePageViewModel LoadViewModel(IEnumerable<SoftwareItemViewModel> softwareItemViewModels)
        {
            return new(softwareItemViewModels);
        }
    }
}
