using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Enums;
using FluentIcons.WinUI;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage.Provider;

namespace AtlasToolbox.Models
{
    public class MultiOptionConfiguration
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public ConfigurationType Type { get; set; }
        public RiskRating RiskRating { get; set; }
        public FluentIcon Icon { get; set; }

        public MultiOptionConfiguration(string name, string key, ConfigurationType type, RiskRating riskRating, FluentIcons.Common.Icon icon = FluentIcons.Common.Icon.Question)
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
