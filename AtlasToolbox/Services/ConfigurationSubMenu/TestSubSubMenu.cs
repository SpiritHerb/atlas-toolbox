using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationSubMenu
{
    public class TestSubSubMenu : IConfigurationSubMenu
    {
        private readonly ConfigurationStoreSubMenu _testSubSubMenu;

        public TestSubSubMenu(
            [FromKeyedServices("TestSubSubMenu")] ConfigurationStoreSubMenu testSubSubMenu)
        {
            _testSubSubMenu = testSubSubMenu;
        }
        public void AddConfigurationService()
        {
            throw new NotImplementedException();
        }
    }
}
