using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AtlasToolbox.Enums;

namespace AtlasToolbox.Models
{
    public class ConfigurationButton
    {
        public ICommand Command { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ConfigurationType Type { get; set; }

        public ConfigurationButton(ICommand command, string name, string description, ConfigurationType type) 
        {
            Command = command;
            Name = name;
            Description = description;
            Type = type;
        }
    }
}