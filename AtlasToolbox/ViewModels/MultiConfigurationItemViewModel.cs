using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Models;
using AtlasToolbox.Stores;
using System.Windows.Input;
using AtlasToolbox.Commands;
using AtlasToolbox.Enums;
using Windows.UI;

namespace AtlasToolbox.ViewModels
{
    public class MultiConfigurationItemViewModel
    {
        private readonly ConfigurationStore _configurationStore;
        private readonly IConfigurationService _configurationService;

        public Configuration Configuration { get; set; }
        public string Name => Configuration.Name;
        public string Key => Configuration.Key;
        public ConfigurationType Type => Configuration.Type;

        public Color Color { get; set; }

        public string RiskRatingString { get; set; }

        private byte _currentSetting;

        public byte CurrentSetting
        {
            get => _currentSetting;
            set
            {
                _currentSetting = value;
                _configurationStore.CurrentSetting = CurrentSetting;
                this.SaveConfigurationCommand.Execute(this);
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

        public Color SetColor(RiskRating riskRating)
        {
            switch (riskRating)
            {
                case RiskRating.HighRisk:
                    return Color.FromArgb(255, 255, 0, 0);
                case RiskRating.MediumRisk:
                    return Color.FromArgb(255, 255, 255, 0);
                case RiskRating.LowRisk:
                    return Color.FromArgb(255, 0, 128, 0);
            }
            return Color.FromArgb(255, 0, 128, 0);
        }

        public string RiskRatingFormatter(RiskRating riskRating)
        {
            string riskRatingString;

            return riskRatingString = riskRating switch
            {
                RiskRating.HighRisk => "High risk",
                RiskRating.MediumRisk => "Medium risk",
                RiskRating.LowRisk => "Low risk",
                _ => throw new System.Exception("Risk rating was not valid")
            };
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
            Color = SetColor(Configuration.RiskRating);
            RiskRatingString = RiskRatingFormatter(Configuration.RiskRating);

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
}
