using AtlasToolbox.Enums;
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
        public ConfigurationSubMenuTypes Type { get; set; }

        public ConfigurationSubMenu(string name, ConfigurationSubMenuTypes type)
        {
            Name = name;
            Type = type;
        }
    }
}
