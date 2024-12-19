using System.Management;

namespace AtlasToolbox.Utils
{
    public class DeviceHelper
    {
        public static bool? GetDeviceStatus(string deviceName)
        {
            using ManagementObjectSearcher searcher = CreateManagementSearcher(deviceName);
            using ManagementObjectCollection devices = searcher.Get();

            if (devices.Count is 0)
            {
                return null;
            }

            foreach (ManagementObject device in devices)
            {
                if (device["Status"] is not "OK")
                {
                    return false;
                }
            }

            return true;
        }

        public static void DisableDevice(string deviceName)
        {
            using ManagementObjectSearcher searcher = CreateManagementSearcher(deviceName);
            using ManagementObjectCollection devices = searcher.Get();

            foreach (ManagementObject device in devices)
            {
                if (device["Status"] is "OK")
                {
                    _ = device.InvokeMethod("Disable", new ManagementObject(), new InvokeMethodOptions());
                }
            }
        }

        public static void EnableDevice(string deviceName)
        {
            using ManagementObjectSearcher searcher = CreateManagementSearcher(deviceName);
            using ManagementObjectCollection devices = searcher.Get();

            foreach (ManagementObject device in devices)
            {
                if (device["Status"] is not "OK")
                {
                    _ = device.InvokeMethod("Enable", new ManagementObject(), new InvokeMethodOptions());
                }
            }
        }

        private static ManagementObjectSearcher CreateManagementSearcher(string deviceName)
        {
            // https://stackoverflow.com/a/75797439
            ManagementNamedValueCollection context = new();
            context.Add("MI_OPERATIONOPTIONS_POWERSHELL_CMDLETNAME", "XXX-PnpDevice");

            EnumerationOptions options = new()
            {
                Context = context
            };

            return new("root\\CIMV2", $"SELECT * FROM Win32_PnPEntity WHERE Name LIKE '{deviceName}'", options);
        }
    }
}
