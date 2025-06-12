using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Enums;

namespace AtlasToolbox.ViewModels
{
    public interface IConfigurationItem
    {
        string Name { get; }
        string Key {  get; }
        ConfigurationType Type { get; }
    }
}
