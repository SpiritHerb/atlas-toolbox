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
        /// <summary>
        /// Check compatibility with app's version
        /// </summary>
        /// <returns></returns>
        public static bool IsCompatible()
        {
            string[] compatibleVersions = ConfigurationManager.AppSettings.Get("AtlasVersion").Split(',');
            string atlasVersion = (string)RegistryHelper.GetValue("HKLM\\SOFTWARE\\AME\\Playbooks\\Applied\\{00000000-0000-4000-6174-6C6173203A33}", "version");
            foreach (string version in compatibleVersions)
            {
                if (atlasVersion == version) return true;
            }
            return false;
        }
    }
}
