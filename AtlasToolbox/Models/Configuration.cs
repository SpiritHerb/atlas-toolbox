using AtlasToolbox.Enums;
using FluentIcons.WinUI;
using Microsoft.UI.Xaml.Controls;

namespace AtlasToolbox.Models
{
    public class Configuration
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public ConfigurationType Type { get; set; }
        public RiskRating RiskRating { get; set; }
        public FluentIcon Icon { get; set; }

        public Configuration(string name, string key, ConfigurationType type, RiskRating riskRating, FluentIcons.Common.Icon icon = FluentIcons.Common.Icon.Question)
        {
            Name = name;
            Key = key;
            Type = type;
            RiskRating = riskRating;
            Icon = new FluentIcon();
            Icon.IconSize = FluentIcons.Common.IconSize.Size48;
            Icon.Icon = icon;
        }
    }
}
