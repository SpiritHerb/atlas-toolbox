using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Stores
{
    public class MultiOptionConfigurationStore
    {
        private byte _currentSetting;

        public byte CurrentSetting
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
