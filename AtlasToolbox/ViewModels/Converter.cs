using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;

namespace AtlasToolbox.ViewModels
{
    internal class BitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            BitmapIcon bitmapIcon = new BitmapIcon();
            // Some icons do not appear as they don't have any, placeholder may be a good idea
            bitmapIcon.UriSource = new Uri($"https://api.winstall.app/icons/next/{value}.webp");
            bitmapIcon.ShowAsMonochrome = false;
            return bitmapIcon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
