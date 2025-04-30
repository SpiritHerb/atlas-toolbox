using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Models
{
    public class RecentToggle
    {
        public DateTime DateTime { get; set; }
        public string Key { get; set; }
        public string OldState { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }

        public RecentToggle(DateTime dateTime, string key, string oldState)
        {
            DateTime = dateTime;
            Key = key;
            OldState = oldState;

            Description = Key + "->"  + oldState;
        }
    }
}
