using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Models
{
    public class Profiles
    {
        public string Name { get; set; }

        public List<string> ConfigurationServices { get; set; }

        public Profiles(
            string name,
            List<string> configurationServices) 
        {
            Name = name;
            ConfigurationServices = configurationServices;
        }

    }
}
