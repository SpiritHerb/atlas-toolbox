using System.Management;

namespace AtlasToolbox.Utils
{
    public class UserAccountHelper
    {
        /// <summary>
        /// Checks for microsoft account
        /// </summary>
        /// <returns></returns>
        public static bool MicrosoftAccountExists()
        {
            using ManagementObjectSearcher searcher = new(@"SELECT * FROM Win32_UserAccount where LocalAccount = false");
            using ManagementObjectCollection result = searcher.Get();
            return result.Count is > 0;
        }
    }
}
