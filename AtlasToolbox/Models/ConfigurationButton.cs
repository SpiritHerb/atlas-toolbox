using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AtlasToolbox.Enums;
using FluentIcons.WinUI;
using Microsoft.UI.Xaml.Controls;

namespace AtlasToolbox.Models
{
    public class ConfigurationButton
    {
        public ICommand Command { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ConfigurationType Type { get; set; }
        public FluentIcon Icon { get; set; }

        public ConfigurationButton(ICommand command, string name, string description, ConfigurationType type, FluentIcons.Common.Icon icon = FluentIcons.Common.Icon.Question) 
        {
            Command = command;
            Name = name;
            Description = description;
            Type = type;
            Icon = new FluentIcon();
            Icon.Icon = icon;
            Icon.IconSize = FluentIcons.Common.IconSize.Size48;
        }
    }
}