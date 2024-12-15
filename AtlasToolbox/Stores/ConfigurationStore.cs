using System;

namespace AtlasToolbox.Stores
{
    public class ConfigurationStore
    {
        private bool _currentSetting;

        public bool CurrentSetting
        {
            get => _currentSetting;
            set
            {
                _currentSetting = value;
                CurrentSettingChanged?.Invoke();
            }
        }

        public event Action? CurrentSettingChanged;
    }
}
