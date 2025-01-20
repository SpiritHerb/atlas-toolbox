using System;
using System.ComponentModel;
using System.Linq;
using AtlasToolbox.ViewModels;
using AtlasToolbox.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;


namespace AtlasToolbox
{
    public class FilterEventHandler : IValueConverter
    {
        public string Type { get; set; }

        public bool Filter(object item)
        {
            return ((ConfigurationItemViewModel)item).Type.ToString() == Type;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return new Predicate<object>(Filter);        
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}
