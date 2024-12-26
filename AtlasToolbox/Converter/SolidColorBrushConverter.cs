using System;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Windows.UI;
//using System.Drawing;

namespace AtlasToolbox.Converter
{
    public class SolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Windows.UI.Color color)
            {
                return new SolidColorBrush(color);
            }
            throw new NotImplementedException();

            //switch ((Color)value)
            //{
            //    case :
            //        return new SolidColorBrush((Colorvalue){

            //        };
            //    case "Red":
            //        return new SolidColorBrush(Microsoft.UI.Colors.Green);
            //    case "Yellow":
            //        return new SolidColorBrush(Microsoft.UI.Colors.Yellow);
            //    default:
            //        return new SolidColorBrush(Microsoft.UI.Colors.Gray);
            //}
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is SolidColorBrush brush) 
            {
                return brush.Color; 
            }
            throw new NotImplementedException();
        }
    }
}
