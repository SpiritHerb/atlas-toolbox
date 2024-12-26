using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Models;
using AtlasToolbox.Stores;
using System.Windows.Input;
using AtlasToolbox.Commands;
using AtlasToolbox.Enums;
using Windows.UI;
using Microsoft.UI.Xaml.Media;
//using System.Drawing;

namespace AtlasToolbox.ViewModels
{
    public class ConfigurationItemViewModel
    {
        private readonly ConfigurationStore _configurationStore;
        private readonly IConfigurationService _configurationService;

        public Configuration Configuration { get; set; }
        public string Name => Configuration.Name;
        public ConfigurationType Type => Configuration.Type;

        //public Color Color { get; set; }

        //public SolidColorBrush TextColor { get; set; }

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

        //public Color SetColor(RiskRating riskRating)
        //{
        //    switch (riskRating)
        //    {
        //        case RiskRating.HighRisk:
        //            return Color.FromArgb(255,255,0,0);
        //        case RiskRating.MediumRisk:
        //            return Color.FromArgb(255, 255, 255, 0);
        //        case RiskRating.LowRisk:
        //            return Color.FromArgb(255, 0, 128, 0);
        //    }
        //    return Color.FromArgb(255, 0, 128, 0);
        //}

        public ConfigurationItemViewModel(
            Configuration configuration,
            ConfigurationStore configurationStore,
            IConfigurationService configurationService)
        {
            Configuration = configuration;

            _configurationStore = configurationStore;
            _configurationService = configurationService;


            _currentSetting = FetchCurrentSetting();

            //Color = SetColor(Configuration.RiskRating);

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
