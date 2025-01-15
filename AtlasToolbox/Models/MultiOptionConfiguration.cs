using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Enums;

namespace AtlasToolbox.Models
{
    public class MultiOptionConfiguration
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public ConfigurationType Type { get; set; }
        public RiskRating RiskRating { get; set; }

        public MultiOptionConfiguration(string name, string key, ConfigurationType type, RiskRating riskRating)
        {
            Name = name;
            Key = key;
            Type = type;
            RiskRating = riskRating;
        }
    }
}
