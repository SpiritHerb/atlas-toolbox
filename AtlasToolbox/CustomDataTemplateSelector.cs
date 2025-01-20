using System;
using AtlasToolbox.ViewModels;
using AtlasToolbox.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;


namespace AtlasToolbox
{
    public class CustomDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ConfigTemplate { get; set; }
    
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            while (container != null && !(container is Frame)) 
            { 
                container = VisualTreeHelper.GetParent(container); 
            }
            if (container is Frame frame)
            {
                Type pageType = frame.SourcePageType;
                switch (pageType.ToString())
                {
                    case "AtlasToolbox.Views.GeneralConfig":
                        if (item is ConfigurationItemViewModel test)
                        {
                            if (test.Type == Enums.ConfigurationType.General)
                            {

                            }
                        }
                        break;
                }
            }
            return base.SelectTemplateCore(item, container);
        }
    }
}
