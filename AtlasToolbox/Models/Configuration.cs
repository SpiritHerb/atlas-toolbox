using AtlasToolbox.Enums;

namespace AtlasToolbox.Models
{
    public class Configuration
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public ConfigurationType Type { get; set; }
        public RiskRating RiskRating { get; set; }

        public Configuration(string name, string key, ConfigurationType type, RiskRating riskRating)
        {
            Name = name;
            Key = key;
            Type = type;
            RiskRating = riskRating;
        }
    }
}
