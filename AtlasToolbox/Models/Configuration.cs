using AtlasToolbox.Enums;
using Microsoft.UI.Xaml.Controls;

namespace AtlasToolbox.Models
{
    public class Configuration
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public ConfigurationType Type { get; set; }
        public RiskRating RiskRating { get; set; }
        public FontIcon Icon { get; set; }

        public Configuration(string name, string key, ConfigurationType type, RiskRating riskRating, string icon = "\uE897")
        {
            Name = name;
            Key = key;
            Type = type;
            RiskRating = riskRating;
            Icon = new FontIcon();
            Icon.Glyph = icon;
        }
    }
}
