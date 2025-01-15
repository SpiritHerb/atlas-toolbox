using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Stores
{
    public class MultiOptionConfigurationStore
    {
        private string _currentSetting;

        private List<string> options;

        public List<string> Options
        {
            get { return options; }
            set { options = value; }
        }

        public string CurrentSetting
        {
            get => _currentSetting;
            set
            {
                _currentSetting = value;
                CurrentSettingChanged?.Invoke();
            }
        }

        public event Action CurrentSettingChanged;
    }
}
