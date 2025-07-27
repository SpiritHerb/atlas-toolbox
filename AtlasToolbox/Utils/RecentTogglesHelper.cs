using System;
using System.Collections.Generic;
using System.IO;
using AtlasToolbox.Models;
using Newtonsoft.Json;

namespace AtlasToolbox.Utils
{
    public class RecentTogglesHelper
    {
        public static List<RecentToggle> recentToggles = new List<RecentToggle>();

        public static void LoadRecentToggles()
        {
            DirectoryInfo profilesDirectory = new DirectoryInfo($"{Environment.GetEnvironmentVariable("windir")}\\AtlasModules\\Toolbox");
            
            FileInfo[] toggleFile = profilesDirectory.GetFiles("togglehistory.json");

            try
            {
                recentToggles = JsonConvert.DeserializeObject<List<RecentToggle>>(File.ReadAllText(toggleFile[0].FullName));
            }catch (Exception e)
            {
                App.logger.Error(e.Message + "History file not found");
            }
        }

        public static void AddRecentToggle(string key, string oldState)
        {
            recentToggles.Add(new RecentToggle(DateTime.Now, key, oldState));
            SaveHistory();
        }

        public static void SaveHistory()
        {
            string jsonString = System.Text.Json.JsonSerializer.Serialize(recentToggles);

            File.WriteAllText($"{Environment.GetEnvironmentVariable("windir")}\\AtlasModules\\Toolbox\\togglehistory.json", jsonString);
        }
    }
}
