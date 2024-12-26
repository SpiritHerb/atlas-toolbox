using AtlasToolbox.Services.ConfigurationServices;
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
using AtlasToolbox.Enums;
using AtlasToolbox.Services.ConfigurationSubMenu;
using System.Drawing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Dispatching;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.CodeDom;

namespace AtlasToolbox.ViewModels
{
    public class ConfigurationItemViewModel
    {
        private readonly ConfigurationStore _configurationStore;
        private readonly IConfigurationService _configurationService;

        public Configuration Configuration { get; set; }
        public string Name => Configuration.Name;
        public ConfigurationType Type => Configuration.Type;
        public RiskRating RiskRating => Configuration.RiskRating;

        public SolidColorBrush TextColor { get; set; }

        public string RiskRatingString => Configuration.RiskRating.ToString();

        private bool _currentSetting;

        public bool CurrentSetting
        {
            get => _currentSetting;
            set
            {
                _currentSetting = value;
                _configurationStore.CurrentSetting = CurrentSetting;
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

        public SolidColorBrush SetColor()
        {    
                SolidColorBrush color = RiskRating.ToString() switch
                {
                    "MediumRisk" => new SolidColorBrush(Microsoft.UI.Colors.Yellow),
                    "LowRisk" => new SolidColorBrush(Microsoft.UI.Colors.Green),
                    "HighRisk" => new SolidColorBrush(Microsoft.UI.Colors.Red),
                    _ => new SolidColorBrush(Microsoft.UI.Colors.Gray),
                };
                return color;
        }

        public ConfigurationItemViewModel(
            Configuration configuration,
            ConfigurationStore configurationStore,
            IConfigurationService configurationService)
        {
            Configuration = configuration;

            _configurationStore = configurationStore;
            _configurationService = configurationService;


            _currentSetting = FetchCurrentSetting();

            TextColor = SetColor();

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
    }
}
