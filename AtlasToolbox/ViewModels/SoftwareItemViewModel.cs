using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Models;
using Microsoft.UI.Xaml.Controls;

namespace AtlasToolbox.ViewModels
{
    public class SoftwareItemViewModel
    {
        public SoftwareItem SoftwareItem { get; set; }

        public string Key => SoftwareItem.Key;
        public string Name => SoftwareItem.Name;
        public string Icon { get; set; }
        public BitmapIcon BitMapIcon { get; set; }

        public SoftwareItemViewModel(SoftwareItem softwareItem)
        {
            SoftwareItem = softwareItem;
            //Icon = $"https://api.winstall.app/icons/next/{Key}.webp";
            BitMapIcon = new BitmapIcon();
            BitMapIcon.UriSource = new Uri($"https://api.winstall.app/icons/next/{Key}.webp");
            BitMapIcon.ShowAsMonochrome = false;
        }
    }
}
