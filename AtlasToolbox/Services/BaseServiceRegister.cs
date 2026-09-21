using AtlasToolbox.Utils;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Services
{
    public class BaseServiceRegister : IRoutable
    {
        public string Key { get; set; } // this is also the name of the service in the atlas core system
        public string Name { get => App.GetValueFromItemList(Key); }
        public string Description { get => App.GetValueFromItemList(Key, true); }
        public string Route { get; set; }
        public string Icon { get; set; }
        public BaseServiceRegister() { }
        /// <summary>
        /// Contstructor for the base registry service.
        /// </summary>
        /// <param name="key">Name of the service in the AtlasToggleLauncher</param>
        /// <param name="route">Where the service should appear in the toolbox</param>
        /// <param name="icon">Icon shown of the item</param>
        public BaseServiceRegister(string key, string route, string icon = "\uE897")
        {
            Key = key;
            Route = route;
            Icon = icon;
        }
    }
}
