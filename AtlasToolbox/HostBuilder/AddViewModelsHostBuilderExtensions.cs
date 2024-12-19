using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Services.ConfigurationSubMenu;
using AtlasToolbox.Enums;
using AtlasToolbox.Models;
using AtlasToolbox.Services;
using AtlasToolbox.Stores;
using AtlasToolbox.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVMEssentials.Services;
using MVVMEssentials.Stores;
using MVVMEssentials.ViewModels;
using System;
using System.Collections.Generic;
using Windows.Services.Maps;
using Windows.Security.Cryptography.Core;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.UI.Xaml;
using System.Reflection.Metadata.Ecma335;
using System.Collections.ObjectModel;

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
            });

            host.AddConfigurationItemViewModels();
            host.AddConfigurationSubMenu();

            return host;
        }

        private static IHostBuilder AddConfigurationSubMenu(this IHostBuilder host)
        {
            // TODO: Change configuration types
            Dictionary<string, ConfigurationSubMenu> configurationDictionary = new()
            {
                ["ContextMenuSubMenu"] = new("Context Menu", "Everything related to the context menu", ConfigurationType.Interface),
                ["AiSubMenu"] = new("AI Features", "Everything related to AI features in Windows 11", ConfigurationType.General),
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
                ["Animations"] = new ("Animations", ConfigurationType.AiSubMenu),
                ["ExtractContextMenu"] = new("Extract context menu", ConfigurationType.ContextMenuSubMenu),
                ["RunWithPriorityContextMenu"] = new("Run With Priority in context menu", ConfigurationType.ContextMenuSubMenu),
                ["Bluetooth"] = new("Bluetooth", ConfigurationType.ServicesSubMenu),
                ["LanmanWorkstation"] = new("Lanman Workstation (SMB)", ConfigurationType.ServicesSubMenu),
                ["NetworkDiscovery"] = new("Network Discovery", ConfigurationType.ServicesSubMenu),
                ["Printing"] = new("Printing", ConfigurationType.ServicesSubMenu),
                ["Troubleshooting"] = new("Troubleshooting", ConfigurationType.Troubleshooting),
                ["CpuIdleContextMenu"] = new("CPU Idle toggle in context menu", ConfigurationType.CpuIdleSubMenu),
                ["LockScreen"] = new("Lock Screen", ConfigurationType.Interface),
                ["ShortcutText"] = new("Shortcut Text", ConfigurationType.Interface),
                ["BootLogo"] = new("Boot Logo", ConfigurationType.BootConfigurationSubMenu),
                ["BootMessages"] = new("Boot Messages", ConfigurationType.BootConfigurationSubMenu),
                ["NewBootMenu"] = new("New Boot Menu", ConfigurationType.BootConfigurationSubMenu),
                ["SpinningAnimation"] = new("Spinning Animation", ConfigurationType.BootConfigurationSubMenu),
                ["AdvancedBootOptions"] = new("Advanced Boot Options on Startup", ConfigurationType.BootConfigurationSubMenu),
                ["AutomaticRepair"] = new("Automatic Repair", ConfigurationType.BootConfigurationSubMenu),
                ["KernelParameters"] = new("Kernel Parameters on Startup", ConfigurationType.BootConfigurationSubMenu),
                ["HighestMode"] = new("Highest Mode", ConfigurationType.BootConfigurationSubMenu),
                ["CompactView"] = new("Compact View", ConfigurationType.FileExplorerSubMenu),
                ["RemovableDrivesInSidebar"] = new("Removable Drives in Sidebar", ConfigurationType.FileExplorerSubMenu),
                ["BackgroundApps"] = new("Background apps", ConfigurationType.FileExplorerSubMenu),
                ["SearchIndexing"] = new("Search Indexing", ConfigurationType.General),
                ["FsoAndGameBar"] = new("FSO and Game Bar", ConfigurationType.General),
                ["AutomaticUpdates"] = new("Automatic updates", ConfigurationType.General),
                ["DeliveryOptimisation"] = new("Delivery optimisation", ConfigurationType.General),
                ["Hibernation"] = new("Hibernation", ConfigurationType.General),
                ["Location"] = new("Location", ConfigurationType.General),
                ["PhoneLink"] = new("Phone link and mobile devices", ConfigurationType.General),
                ["PowerSaving"] = new("Power saving", ConfigurationType.General),
                ["Sleep"] = new("Sleep", ConfigurationType.General),
                ["AppStoreArchiving"] = new("Microsoft Store Archiving", ConfigurationType.General),
                ["SystemRestore"] = new("System Restore", ConfigurationType.General),
                ["UpdateNotifications"] = new("Update Notifications", ConfigurationType.General),
                ["WebSearch"] = new("Start Menu Web Search", ConfigurationType.General),
                ["Widgets"] = new("Desktop widgets", ConfigurationType.General),
                ["WindowsSpotlight"] = new("Windows Spotlight", ConfigurationType.General),
                ["AppStoreArchiving"] = new("Microsoft Store archiving", ConfigurationType.General),

            };

            host.ConfigureServices((_,services) =>
            {
                services.AddSingleton<IEnumerable<ConfigurationItemViewModel>>(provider =>
                {
                    List<ConfigurationItemViewModel> viewModels = new();

                    foreach (KeyValuePair<string, Configuration> item in configurationDictionary)
                    {
                        if (
                        item.Value.Type != ConfigurationType.General &&
                        item.Value.Type != ConfigurationType.Interface &&
                        item.Value.Type != ConfigurationType.Security &&
                        item.Value.Type != ConfigurationType.Performance &&
                        item.Value.Type != ConfigurationType.Privacy &&
                        item.Value.Type != ConfigurationType.Advanced &&
                        item.Value.Type != ConfigurationType.Troubleshooting)
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
