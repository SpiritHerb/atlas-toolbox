using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Enums;
using AtlasToolbox.Models;
using AtlasToolbox.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AtlasToolbox.Commands;
using MVVMEssentials.Services;

namespace AtlasToolbox.ViewModels
{
    public class ConfigurationItemViewModel
    {
        private readonly ConfigurationStore _configurationStore;
        private readonly IConfigurationService _configurationService;

        public Configuration Configuration { get; set; }
        public string Name => Configuration.Name;
        public ConfigurationType Type => Configuration.Type;

        private bool _currentSetting;

        public bool CurrentSetting
        {
            get => _currentSetting;
            set
            {
                _currentSetting = value;
            }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
            }
        }

        public ICommand SaveConfigurationCommand { get; }
        public ICommand NavigateConfigurationItemMenuCommand { get; }

        public ConfigurationItemViewModel(
            Configuration configuration,
            ConfigurationStore configurationStore,
            IConfigurationService configurationService)
        {
            Configuration = configuration;

            _configurationStore = configurationStore;
            _configurationService = configurationService;


            _currentSetting = FetchCurrentSetting();
            _configurationStore.CurrentSettingChanged += ConfigurationStore_CurrentSettingChanged;

            SaveConfigurationCommand = new SaveConfigurationCommand(this, configurationStore, configurationService);

        }

        public bool FetchCurrentSetting()
        {
            IsBusy = true;

            try
            {
                bool currentSetting = _configurationService.IsEnabled();
                _configurationStore.CurrentSetting = currentSetting;
                return currentSetting;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void ConfigurationStore_CurrentSettingChanged()
        {
            CurrentSetting = _configurationStore.CurrentSetting;
        }
    }
}
