//using Microsoft.Extensions.Hosting;
//using System;
//using Microsoft.Extensions.DependencyInjection;
//using System.Collections.Generic;
//using AtlasToolbox.Models;
//using AtlasToolbox.Services.ConfigurationServices;
//using AtlasToolbox.Enums;
//using AtlasToolbox.Services.ConfigurationServices;
//using AtlasToolbox.Services.ConfigurationServicesParent;
//using AtlasToolbox.ViewModels;
//using AtlasToolbox.Stores;

//namespace AtlasToolbox.HostBuilder
//{
//    public static class AddConfigurationServices
//    {

//        public static ConfigurationItemMenuViewModel AddConfigurationServices()
//        {
//            Dictionary<string, Configuration> configurationServices = new()
//            {
//                ["AppStoreArchiving"] = new("App Store Archiving", new AppStoreArchivingConfigurationService(), ConfigurationType.General),
//            };

//            List<ConfigurationItemViewModel> viewModels = new();

//            {
//                foreach (KeyValuePair<string, Configuration> item in configurationServices)
//                {

//                    ConfigurationItemViewModel viewModel = CreateConfigurationItemViewModel(item.Key, item.Value);
//                    viewModels.Add(viewModel);
//                }
//            });

//        }

//        private static ConfigurationItemViewModel CreateConfigurationItemViewModel(
//           object? key, Configuration configuration)
//        {
//            ConfigurationStore configurationStore = <ConfigurationStore>(key);
//            IConfigurationService configurationService = serviceProvider.GetRequiredKeyedService<IConfigurationService>(key);

//            ConfigurationItemViewModel viewModel = new(
//                configuration, configurationStore, configurationService);

//            return viewModel;
//        }
//    }
//}
