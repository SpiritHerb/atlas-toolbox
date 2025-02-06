using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Enums;
using Windows.Storage.Provider;

namespace AtlasToolbox.Models
{
    public class Links
    {
        public string link {  get; set; }
        public string name { get; set; }
        public ConfigurationType configurationType { get; set; }

        public Links(string link, string name, ConfigurationType configurationType)
        {
            this.link = link;
            this.name = name;
            this.configurationType = configurationType;
        }
    }
}
