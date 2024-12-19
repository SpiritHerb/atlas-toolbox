using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AtlasToolbox.Utils
{
    public class ItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DetailTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return DetailTemplate;
        }
    }
}
