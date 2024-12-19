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
                ["ContextMenuSubMenu"] = new("Context Menu", "Everything related to the context menu", ConfigurationType.General),
                ["AiSubMenu"] = new("AI Features", "Everything related to AI features in Windows 11", ConfigurationType.General),
                ["ServicesSubMenu"] = new("Services", "Everything related to services in Windows", ConfigurationType.Advanced),
                ["CPUIdleSubMenu"] = new("CPU idle", "Everything related to CPU idling in Windows", ConfigurationType.General),
                ["BootConfigurationSubMenu"] = new("Boot configuration", "Everything related to booting in Windows", ConfigurationType.Advanced),
                ["FileExplorerSubMenu"] = new("File Explorer customization", "Everything related to customizing the Windows File Explorer", ConfigurationType.Interface),
            };
            host.ConfigureServices((_, services) =>
            {
                services.AddSingleton<IEnumerable<ConfigurationSubMenuViewModel>>(provider =>
                {
                    List<ConfigurationSubMenuViewModel> viewModels = new();
                    List<ConfigurationItemViewModel> itemViewModels = new();
                    IEnumerable<ConfigurationItemViewModel> items;

                    foreach (KeyValuePair<string, ConfigurationSubMenu> item in configurationDictionary)
                    {
                        foreach (ConfigurationItemViewModel configurationItemViewModel in subMenuOnlyItems)
                        {
                            if (configurationItemViewModel.Type.ToString() == item.Key)
                            {
                                itemViewModels.Add(configurationItemViewModel);
                            }
                        }
                        items = itemViewModels;
                        ConfigurationSubMenuViewModel viewModel = CreateConfigurationSubMenuViewModel(provider, items, item.Key, item.Value);
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
                ["Animations"] = new ("Animations", ConfigurationType.Interface),
                ["Bluetooth"] = new("Bluetooth", ConfigurationType.ServicesSubMenu),
                ["FsoAndGameBar"] = new("FSO and Game Bar", ConfigurationType.General),
                //["WindowsFirewall"] = new("Firewall", ConfigurationType.Privacy),
                //["GameMode"] = new("Game Mode", ConfigurationType.Performance),
                //["Hags"] = new("HAGS", ConfigurationType.Advanced),
                ["LanmanWorkstation"] = new("Lanman Workstation (SMB)", ConfigurationType.ServicesSubMenu),
                //["MicrosoftStore"] = new("Microsoft Store", ConfigurationType.Customization),
                ["NetworkDiscovery"] = new("Network Discovery", ConfigurationType.ServicesSubMenu),
                //["Notifications"] = new("Notifications", ConfigurationType.Customization),
                ["Printing"] = new("Printing", ConfigurationType.ServicesSubMenu),
                ["SearchIndexing"] = new("Search Indexing", ConfigurationType.General),
                ["Troubleshooting"] = new("Troubleshooting", ConfigurationType.Troubleshooting),
                //["Uwp"] = new("UWP", ConfigurationType.Customization),
                //["Vpn"] = new("VPN", ConfigurationType.Customization),
                //["ModernAltTab"] = new("Modern Alt-Tab", ConfigurationType.Customization),
                ["CpuIdleContextMenu"] = new("CPU Idle toggle in context menu", ConfigurationType.CpuIdleSubMenu),
                //["DarkTitlebars"] = new("Dark Titlebars", ConfigurationType.Customization),
                ["LockScreen"] = new("Lock Screen", ConfigurationType.Interface),
                //["ModernVolumeFlyout"] = new("Modern Volume Flyout", ConfigurationType.Customization),
                ["RunWithPriorityContextMenu"] = new("Run With Priority in context menu", ConfigurationType.ContextMenuSubMenu),
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
                //["QuickAccess"] = new("Quick Access", ConfigurationType.Advanced),
                ["RemovableDrivesInSidebar"] = new("Removable Drives in Sidebar", ConfigurationType.FileExplorerSubMenu),
                //["Copilot"] = new("Copilot feature", ConfigurationType.Privacy),
                ["AutomaticUpdates"] = new("Automatic updates", ConfigurationType.General),
                ["BackgroundApps"] = new("Background apps", ConfigurationType.FileExplorerSubMenu),
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
                ["ExtractContextMenu"] = new("Extract context menu", ConfigurationType.ContextMenuSubMenu),
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
                        item.Value.Type != ConfigurationType.Advanced)
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
          IServiceProvider serviceProvider, IEnumerable<ConfigurationItemViewModel> configurationItemViewModels, object key, ConfigurationSubMenu configuration)
        {
            ConfigurationStoreSubMenu configurationStoreSubMenu = serviceProvider.GetRequiredKeyedService<ConfigurationStoreSubMenu>(key);

            ConfigurationSubMenuViewModel  viewModel = new(
               configuration, configurationStoreSubMenu, configurationItemViewModels);

            return viewModel;
        }
    }
}
