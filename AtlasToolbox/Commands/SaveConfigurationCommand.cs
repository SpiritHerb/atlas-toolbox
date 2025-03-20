using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Stores;
using AtlasToolbox.ViewModels;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using System.Threading.Tasks;

namespace AtlasToolbox.Commands
{
    public class SaveConfigurationCommand : AsyncCommandBase
    {
        private readonly ConfigurationItemViewModel _configurationItemViewModel;
        private readonly ConfigurationStore _configurationStore;
        private readonly IConfigurationService _configurationService;

        public SaveConfigurationCommand(
            ConfigurationItemViewModel configurationItemViewModel,
            ConfigurationStore configurationStore,
            IConfigurationService configurationService)
        {
            _configurationItemViewModel = configurationItemViewModel;
            _configurationStore = configurationStore;
            _configurationService = configurationService;
        }

        /// <summary>
        /// Saves the current state of a ConfigurationService
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(object parameter)
        {
            bool currentSetting = _configurationStore.CurrentSetting;

            _configurationItemViewModel.IsBusy = true;

            try
            {
                await Task.Run(currentSetting
                    ? _configurationService.Enable
                    : _configurationService.Disable);
            }
            finally
            {
                _configurationItemViewModel.IsBusy = false;
            }
        }
    }
}
