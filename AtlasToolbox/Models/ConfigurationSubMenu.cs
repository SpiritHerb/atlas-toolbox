using AtlasToolbox.Enums;
using FluentIcons.WinUI;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Models
{
    public class ConfigurationSubMenu
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ConfigurationType Type { get; set; }
        public FluentIcon Icon { get; set; }

        public ConfigurationSubMenu(string name, string description, ConfigurationType type, FluentIcons.Common.Icon icon = FluentIcons.Common.Icon.Question)
        {
            Name = name;
            Description = description;
            Type = type;
            Icon = new FluentIcon();
            Icon.IconSize = FluentIcons.Common.IconSize.Size48;
            Icon.Icon = icon;
        }
    }
}
