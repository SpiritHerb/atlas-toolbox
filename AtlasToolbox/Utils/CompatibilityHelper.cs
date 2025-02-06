using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Utils
{
    public class CompatibilityHelper
    {
        public static bool IsCompatible()
        {
            string toolboxVersion = ConfigurationManager.AppSettings.Get("AtlasVersion");
            string atlasVersion = (string)RegistryHelper.GetValue("HKLM\\SOFTWARE\\AME\\Playbooks\\Applied\\{00000000-0000-4000-6174-6C6173203A33}", "version");
            if (toolboxVersion == atlasVersion)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
