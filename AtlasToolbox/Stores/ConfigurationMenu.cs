using AtlasToolbox.Services.ConfigurationServices;
using System.Collections.Generic;


namespace AtlasToolbox.Stores
{
    public class ConfigurationMenu
    {
        private List<string> _configurationStores;

        public List<string> ConfigurationStores
        {
            get
            {
                return _configurationStores;
            }
            set
            {
                _configurationStores = value;
            }
        }
    }
}
