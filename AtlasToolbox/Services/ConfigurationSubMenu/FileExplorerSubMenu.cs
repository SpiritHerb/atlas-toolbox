using AtlasToolbox.Stores;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Services.ConfigurationSubMenu
{
    public class FileExplorerSubMenu : IConfigurationSubMenu
    {
        private readonly ConfigurationStoreSubMenu _fileExplorerSubMenu;

        public FileExplorerSubMenu(
            [FromKeyedServices("FileExplorerSubMenu")] ConfigurationStoreSubMenu fileExplorerSubMenu)
        {
            _fileExplorerSubMenu = fileExplorerSubMenu;
        }

        public void AddConfigurationService()
        {
            throw new NotImplementedException();
        }
    }
}
