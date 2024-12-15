using AtlasToolbox.Enums;

namespace AtlasToolbox.Models
{
    public class Configuration
    {
        public string Name { get; set; }
        public ConfigurationType Type { get; set; }

        public Configuration(string name, ConfigurationType type)
        {
            Name = name;
            Type = type;
        }
    }
}
