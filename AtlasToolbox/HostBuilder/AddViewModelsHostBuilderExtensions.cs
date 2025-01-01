using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Models;
using AtlasToolbox.Stores;
using AtlasToolbox.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVMEssentials.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AtlasToolbox.Enums;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Graphics.Canvas.Text;

namespace AtlasToolbox.HostBuilder
{
    public static class AddViewModelsHostBuilderExtensions
    {
        private static List<ConfigurationItemViewModel> subMenuOnlyItems = new List<ConfigurationItemViewModel>();
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices((_, services) =>
            {
                services.AddSingleton<MainViewModel>();
                services.AddTransient(CreateGeneralConfigViewModel);
                services.AddTransient(CreateInterfaceTweaksViewModel);
                services.AddTransient(CreateAdvancedConfigViewModel);
                services.AddTransient(CreateSecurityConfigViewModel);
                services.AddTransient(CreateWindowsSettingsViewModel);
                services.AddTransient(CreateTroubleshootingViewModel);
                services.AddTransient(CreateHomePageViewModel);
            });

             host.AddConfigurationItemViewModels();
             host.AddConfigurationSubMenu();
             host.AddProfiles();

            return host;
        }


        private static IHostBuilder AddProfiles(this IHostBuilder host)
        {
            List<Profiles> configurationDictionary = new List<Profiles>();

            DirectoryInfo profilesDirectory = new DirectoryInfo("..\\..\\..\\..\\Profiles\\");
            FileInfo[] profileFile = profilesDirectory.GetFiles();

            foreach (FileInfo file in profileFile)
            {
                bool loop = true;
                List<string> keys = new List<string>();
                string profileKeyname = file.Name;
                string profileName;
                using (StreamReader profile = new StreamReader(file.FullName, Encoding.UTF8))
                {
                    profileName = profile.ReadLine();
                    while (loop)
                    {
                        //string configurationServiceKey = profile.ReadLine();
                        string key = profile.ReadLine();
                        if (key == null) { loop = false; }
                        else { keys.Add(key); }
                    }
                }
                configurationDictionary.Add(new(profileName, profileKeyname,keys));
            };
            host.ConfigureServices((_, services) =>
            {
                services.AddSingleton<IEnumerable<Profiles>> (provider =>
                {
                    return configurationDictionary;
                });
            });
            return host;
        }


        private static IHostBuilder AddConfigurationSubMenu(this IHostBuilder host)
        {
            // TODO: Change configuration types
            Dictionary<string, ConfigurationSubMenu> configurationDictionary = new()
            {
                ["ContextMenuSubMenu"] = new("Context Menu", "Everything related to the context menu", ConfigurationType.Interface),
                ["AiSubMenu"] = new("AI Features", "Everything related to AI features in Windows 11", ConfigurationType.Windows),
                ["ServicesSubMenu"] = new("Services", "Everything related to services in Windows", ConfigurationType.Advanced),
                ["CPUIdleSubMenu"] = new("CPU idle", "Everything related to CPU idling in Windows", ConfigurationType.Advanced),
                ["BootConfigurationSubMenu"] = new("Boot configuration", "Everything related to booting in Windows", ConfigurationType.Advanced),
                ["FileExplorerSubMenu"] = new("File Explorer customization", "Everything related to customizing the Windows File Explorer", ConfigurationType.Interface),
            };
            host.ConfigureServices((_, services) =>
            {
                services.AddSingleton<IEnumerable<ConfigurationSubMenuViewModel>>(provider =>
                {
                    List<ConfigurationSubMenuViewModel> viewModels = new();

                    foreach (KeyValuePair<string, ConfigurationSubMenu> item in configurationDictionary)
                    {
                        ObservableCollection<ConfigurationItemViewModel> itemViewModels = new();
                        foreach (ConfigurationItemViewModel configurationItemViewModel in subMenuOnlyItems)
                        {
                            if (configurationItemViewModel.Type.ToString() == item.Key) itemViewModels.Add(configurationItemViewModel);
                        }
                        ConfigurationSubMenuViewModel viewModel = CreateConfigurationSubMenuViewModel(provider, itemViewModels, item.Key, item.Value);
                        viewModels.Add(viewModel);
                    }
                    return viewModels;
                });
            });

            return host;
        }

        private static IHostBuilder AddConfigurationItemViewModels(this IHostBuilder host)
        {
            // TODO: Change configuration types
            Dictionary<string, Configuration> configurationDictionary = new()
            {
                ["TestConfig"] = new ("TestConfig", "TestConfig", ConfigurationType.AiSubMenu, RiskRating.HighRisk),
                ["OtherTestConfig"] = new("Other test config", "OtherTestConfig", ConfigurationType.General, RiskRating.HighRisk),
                ["Animations"] = new ("Animations", "Animations", ConfigurationType.Interface, RiskRating.LowRisk),
                ["ExtractContextMenu"] = new("Extract context menu", "ExtractContextMenu", ConfigurationType.ContextMenuSubMenu, RiskRating.LowRisk),
                ["RunWithPriorityContextMenu"] = new("Run With Priority in context menu", "RunWithPriorityContextMenu", ConfigurationType.ContextMenuSubMenu, RiskRating.MediumRisk),
                ["Bluetooth"] = new("Bluetooth", "Bluetooth", ConfigurationType.ServicesSubMenu, RiskRating.LowRisk),
                ["LanmanWorkstation"] = new("Lanman Workstation (SMB)", "LanmanWorkstation", ConfigurationType.ServicesSubMenu, RiskRating.HighRisk),
                ["NetworkDiscovery"] = new("Network Discovery", "NetworkDiscovery", ConfigurationType.ServicesSubMenu, RiskRating.LowRisk),
                ["Printing"] = new("Printing", "Printing", ConfigurationType.ServicesSubMenu, RiskRating.LowRisk),
                ["Troubleshooting"] = new("Troubleshooting", "Troubleshooting", ConfigurationType.Troubleshooting, RiskRating.MediumRisk),
                ["CpuIdleContextMenu"] = new("CPU Idle toggle in context menu", "CpuIdleContextMenu", ConfigurationType.CpuIdleSubMenu, RiskRating.LowRisk),
                ["LockScreen"] = new("Lock Screen", "LockScreen", ConfigurationType.Interface, RiskRating.LowRisk),
                ["ShortcutText"] = new("Shortcut Text", "ShortcutText", ConfigurationType.Interface, RiskRating.LowRisk),
                ["BootLogo"] = new("Boot Logo", "BootLogo", ConfigurationType.BootConfigurationSubMenu, RiskRating.LowRisk),
                ["BootMessages"] = new("Boot Messages", "BootMessages", ConfigurationType.BootConfigurationSubMenu, RiskRating.LowRisk),
                ["NewBootMenu"] = new("New Boot Menu", "NewBootMenu", ConfigurationType.BootConfigurationSubMenu, RiskRating.LowRisk),
                ["SpinningAnimation"] = new("Spinning Animation", "SpinningAnimations", ConfigurationType.BootConfigurationSubMenu, RiskRating.LowRisk),
                ["AdvancedBootOptions"] = new("Advanced Boot Options on Startup", "AdvancedBootOptions", ConfigurationType.BootConfigurationSubMenu, RiskRating.LowRisk),
                ["AutomaticRepair"] = new("Automatic Repair", "AutomaticRepair", ConfigurationType.BootConfigurationSubMenu, RiskRating.LowRisk),
                ["KernelParameters"] = new("Kernel Parameters on Startup", "KernelParameters", ConfigurationType.BootConfigurationSubMenu, RiskRating.LowRisk),
                ["HighestMode"] = new("Highest Mode", "HighestMode", ConfigurationType.BootConfigurationSubMenu, RiskRating.LowRisk),
                ["CompactView"] = new("Compact View", "CompactView", ConfigurationType.FileExplorerSubMenu, RiskRating.LowRisk),
                ["RemovableDrivesInSidebar"] = new("Removable Drives in Sidebar", "RemovableDrivesInSidebar", ConfigurationType.FileExplorerSubMenu, RiskRating.LowRisk),
                ["BackgroundApps"] = new("Background apps", "BackgroundApps", ConfigurationType.FileExplorerSubMenu, RiskRating.LowRisk),
                ["SearchIndexing"] = new("Search Indexing", "SearchIndexing", ConfigurationType.General, RiskRating.LowRisk),
                ["FsoAndGameBar"] = new("FSO and Game Bar", "FsoAndGameBar", ConfigurationType.General, RiskRating.LowRisk),
                ["AutomaticUpdates"] = new("Automatic updates", "AutomaticUpdates", ConfigurationType.General, RiskRating.LowRisk),
                ["DeliveryOptimisation"] = new("Delivery optimisation", "DeliveryOptimisation", ConfigurationType.General, RiskRating.LowRisk),
                ["Hibernation"] = new("Hibernation", "Hibernation", ConfigurationType.General, RiskRating.LowRisk),
                ["Location"] = new("Location", "Location", ConfigurationType.General, RiskRating.LowRisk),
                ["PhoneLink"] = new("Phone link and mobile devices", "PhoneLink", ConfigurationType.General, RiskRating.LowRisk),
                ["PowerSaving"] = new("Power saving", "PowerSaving", ConfigurationType.General, RiskRating.LowRisk),
                ["Sleep"] = new("Sleep", "Sleep", ConfigurationType.General, RiskRating.LowRisk),
                ["SystemRestore"] = new("System Restore", "SystemRestore", ConfigurationType.General, RiskRating.LowRisk),
                ["UpdateNotifications"] = new("Update Notifications", "UpdateNotifications", ConfigurationType.General, RiskRating.LowRisk),
                ["WebSearch"] = new("Start Menu Web Search", "WebSearch", ConfigurationType.General, RiskRating.LowRisk),
                ["Widgets"] = new("Desktop widgets", "Widgets", ConfigurationType.General, RiskRating.LowRisk),
                ["WindowsSpotlight"] = new("Windows Spotlight", "WindowsSpotlight", ConfigurationType.General, RiskRating.LowRisk),
                ["AppStoreArchiving"] = new("Microsoft Store archiving", "AppStoreArchiving", ConfigurationType.General, RiskRating.LowRisk),
                ["TakeOwnership"] = new("Add \"Take Ownership\" in the context menu", "TakeOwnership", ConfigurationType.ContextMenuSubMenu, RiskRating.HighRisk),

            };

            host.ConfigureServices((_,services) =>
            {
                services.AddSingleton<IEnumerable<ConfigurationItemViewModel>>(provider =>
                {
                    List<ConfigurationItemViewModel> viewModels = new();

                    foreach (KeyValuePair<string, Configuration> item in configurationDictionary)
                    {
                        if (
                        item.Value.Type >= (ConfigurationType)7)
                        {
                            subMenuOnlyItems.Add(CreateConfigurationItemViewModel(provider, item.Key, item.Value));
                        }else
                        {
                            ConfigurationItemViewModel viewModel = CreateConfigurationItemViewModel(provider, item.Key, item.Value);
                            viewModels.Add(viewModel);
                        }
                    }
                    return viewModels;
                });
            });

            return host;
        }

        private static ConfigurationItemViewModel CreateConfigurationItemViewModel(
            IServiceProvider serviceProvider, object key, Configuration configuration)
        {
                ConfigurationStore configurationStore = serviceProvider.GetRequiredKeyedService<ConfigurationStore>(key);
                IConfigurationService configurationService = serviceProvider.GetRequiredKeyedService<IConfigurationService>(key);

                ConfigurationItemViewModel viewModel = new(
                    configuration, configurationStore, configurationService);

                return viewModel;
        }

        #region Create ViewModels
        private static GeneralConfigViewModel CreateGeneralConfigViewModel(IServiceProvider serviceProvider)
        {
            return GeneralConfigViewModel.LoadViewModel(
                serviceProvider.GetServices<ConfigurationItemViewModel>(),
                serviceProvider.GetServices<ConfigurationSubMenuViewModel>());
        }

        private static InterfaceTweaksViewModel CreateInterfaceTweaksViewModel(IServiceProvider serviceProvider)
        {
            return InterfaceTweaksViewModel.LoadViewModel(
                serviceProvider.GetServices<ConfigurationItemViewModel>(),
                serviceProvider.GetServices<ConfigurationSubMenuViewModel>());
        }

        private static WindowsSettingsViewModel CreateWindowsSettingsViewModel(IServiceProvider serviceProvider)
        {
            return WindowsSettingsViewModel.LoadViewModel(
                serviceProvider.GetServices<ConfigurationItemViewModel>(),
                serviceProvider.GetServices<ConfigurationSubMenuViewModel>());
        }

        private static AdvancedConfigViewModel CreateAdvancedConfigViewModel(IServiceProvider serviceProvider)
        {
            return AdvancedConfigViewModel.LoadViewModel(
                serviceProvider.GetServices<ConfigurationItemViewModel>(),
                serviceProvider.GetServices<ConfigurationSubMenuViewModel>());
        }
        private static SecurityConfigViewModel CreateSecurityConfigViewModel(IServiceProvider serviceProvider)
        {
            return SecurityConfigViewModel.LoadViewModel(
                serviceProvider.GetServices<ConfigurationItemViewModel>(),
                serviceProvider.GetServices<ConfigurationSubMenuViewModel>());
        }
        private static TroubleshootingViewModel CreateTroubleshootingViewModel(IServiceProvider serviceProvider)
        {
            return TroubleshootingViewModel.LoadViewModel(
                serviceProvider.GetServices<ConfigurationItemViewModel>(),
                serviceProvider.GetServices<ConfigurationSubMenuViewModel>());
        }

        private static HomePageViewModel CreateHomePageViewModel(IServiceProvider serviceProvider)
        {
            return HomePageViewModel.LoadViewModel(
                serviceProvider.GetServices<Profiles>(),
                serviceProvider.GetServices<ConfigurationItemViewModel>());
        }
        #endregion Create ViewModels

        private static ConfigurationSubMenuViewModel CreateConfigurationSubMenuViewModel(
          IServiceProvider serviceProvider, ObservableCollection<ConfigurationItemViewModel> configurationItemViewModels, object key, ConfigurationSubMenu configuration)
        {
            ConfigurationStoreSubMenu configurationStoreSubMenu = serviceProvider.GetRequiredKeyedService<ConfigurationStoreSubMenu>(key);

            ConfigurationSubMenuViewModel  viewModel = new(
               configuration, configurationStoreSubMenu, configurationItemViewModels);

            return viewModel;
        }
    }
}
