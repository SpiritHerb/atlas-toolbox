using AtlasToolbox.Utils;
using System.ServiceProcess;

namespace AtlasToolbox.Utils
{
    public class ServiceHelper
    {
        public static ServiceStartMode GetStartupType(string serviceName)
        {
            using ServiceController serviceController = new(serviceName);
            return serviceController.StartType;
        }

        public static void SetStartupType(string serviceName, ServiceStartMode startupType)
        {
            string keyName = $@"HKLM\SYSTEM\CurrentControlSet\Services\{serviceName}";
            RegistryHelper.SetValue(keyName, "Start", (int)startupType);
        }

        public static bool IsStartupTypeMatch(string serviceName, ServiceStartMode startupType)
        {
            return GetStartupType(serviceName) == startupType;
        }
    }
}
