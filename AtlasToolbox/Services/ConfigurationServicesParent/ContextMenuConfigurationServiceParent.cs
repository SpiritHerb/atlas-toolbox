using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Services.ConfigurationServicesParent
{
    public class ContextMenuConfigurationServiceParent : IConfigurationMenu
    {
        private readonly ConfigurationMenu _contextMenuConfigurationMenu;


        public ContextMenuConfigurationServiceParent() { }

        public ContextMenuConfigurationServiceParent(ConfigurationMenu contextMenuConfigurationMenu)
        {
            _contextMenuConfigurationMenu = contextMenuConfigurationMenu;
            AddConfigurationService();
        }

        public void AddConfigurationService()
        {
            _contextMenuConfigurationMenu.ConfigurationStores = new List<string> {
                    "Animations" };
        }
    }
}
