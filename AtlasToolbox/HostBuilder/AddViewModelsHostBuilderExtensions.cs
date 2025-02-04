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
using System.Linq;
using System.Threading.Tasks;

namespace AtlasToolbox.HostBuilder
{
    public static class AddViewModelsHostBuilderExtensions
    {
        private static List<Object> subMenuOnlyItems = new List<Object>();
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices((_, services) =>
            {
                services.AddSingleton<MainViewModel>();
                services.AddTransient(CreateConfigPageViewModel);
                services.AddTransient(CreateHomePageViewModel);
                services.AddTransient(CreateSoftwarePageViewModel);
            });

            host.AddLinksItemViewModels();
            host.AddSoftwareItemsViewModels();
            host.AddMultiOptionConfigurationViewModels();
            host.AddConfigurationItemViewModels();
            host.AddConfigurationSubMenu();
            host.AddProfiles();

            return host;
        }

        private static IHostBuilder AddSoftwareItemsViewModels(this IHostBuilder host)
        {
            Dictionary<string, SoftwareItem> configurationDictionary = new()
            {
                ["Ungoogled Chromium"] = new("Ungoogled Chromium", "eloston.ungoogled-chromium"),
                ["Google Chrome"] = new("Google Chrome", "Google.Chrome"),
                ["Mozilla Firefox"] = new("Mozilla Firefox", "Mozilla.Firefox"),
                ["Waterfox"] = new("Waterfox", "Waterfox.Waterfox"),
                ["Brave Browser"] = new("Brave Browser", "Brave.Brave"),
                ["LibreWolf"] = new("LibreWolf", "LibreWolf.LibreWolf"),
                ["Tor Browser"] = new("Tor Browser", "TorProject.TorBrowser"),
                ["Discord"] = new("Discord", "Discord.Discord"),
                ["Discord Canary"] = new("Discord Canary", "Discord.Discord.Canary"),
                ["Steam"] = new("Steam", "Valve.Steam"),
                ["Playnite"] = new("Playnite", "Playnite.Playnite"),
                ["Heroic"] = new("Heroic", "HeroicGamesLauncher.HeroicGamesLauncher"),
                ["Everything"] = new("Everything", "voidtools.Everything"),
                ["Mozilla Thunderbird"] = new("Mozilla Thunderbird", "Mozilla.Thunderbird"),
                ["IrfanView"] = new("IrfanView", "IrfanSkiljan.IrfanView"),
                ["Git"] = new("Git", "Git.Git"),
                ["VLC"] = new("VLC", "VideoLAN.VLC"),
                ["PuTTY"] = new("PuTTY", "PuTTY.PuTTY"),
                ["Ditto"] = new("Ditto", "Ditto.Ditto"),
                ["7-Zip"] = new("7-Zip", "7zip.7zip"),
                ["Teamspeak"] = new("Teamspeak", "TeamSpeakSystems.TeamSpeakClient"),
                ["Spotify"] = new("Spotify", "Spotify.Spotify"),
                ["OBS Studio"] = new("OBS Studio", "OBSProject.OBSStudio"),
                ["MSI Afterburner"] = new("MSI Afterburner", "Guru3D.Afterburner"),
                ["foobar2000"] = new("foobar2000", "PeterPawlowski.foobar2000"),
                ["CPU-Z"] = new("CPU-Z", "CPUID.CPU-Z"),
                ["GPU-Z"] = new("GPU-Z", "TechPowerUp.GPU-Z"),
                ["Notepad++"] = new("Notepad++", "Notepad++.Notepad++"),
                ["VSCode"] = new("VSCode", "Microsoft.VisualStudioCode"),
                ["VSCodium"] = new("VSCodium", "VSCodium.VSCodium"),
                ["BCUninstaller"] = new("BCUninstaller", "Klocman.BulkCrapUninstaller"),
                ["HWiNFO"] = new("HWiNFO", "REALiX.HWiNFO"),
                ["Lightshot"] = new("Lightshot", "Skillbrains.Lightshot"),
                ["ShareX"] = new("ShareX", "ShareX.ShareX"),
                ["Snipping Tool"] = new("Snipping Tool", "9MZ95KL8MR0L"),
                ["ExplorerPatcher"] = new("ExplorerPatcher", "valinet.ExplorerPatcher"),
                ["Powershell 7"] = new("Powershell 7", "Microsoft.PowerShell"),
                ["UniGetUI"] = new("UniGetUI", "MartiCliment.UniGetUI"),
            };

            host.ConfigureServices((_, services) =>
            {
                services.AddSingleton<IEnumerable<SoftwareItemViewModel>>(provider =>
                {
                    List<SoftwareItemViewModel> viewModels = new();

                    foreach (KeyValuePair<string, SoftwareItem> item in configurationDictionary)
                    {
                        viewModels.Add(CreateSoftwareItemViewModel(item.Value));
                    }
                    return viewModels;
                });
            });
            return host;
        }


        private static IHostBuilder AddLinksItemViewModels(this IHostBuilder host)
        {
            Dictionary<string, Links> configurationDictionary = new()
            {
                ["ExplorerPatcher"] = new ("https://github.com/valinet/ExplorerPatcher", "ExplorerPatcher", ConfigurationType.StartMenuSubMenu),
                ["StartAllBack"] = new ("https://www.startallback.com/", "StartAllBack", ConfigurationType.StartMenuSubMenu),
                ["OpenShellTest"] = new (@"ms-settings:activation", "test cmd file", ConfigurationType.StartMenuSubMenu),
            };

            host.ConfigureServices((_, services) =>
            {
                services.AddSingleton<IEnumerable<LinksViewModel>>(provider =>
                {
                    List<LinksViewModel> viewModels = new();

                    foreach (KeyValuePair<string, Links> item in configurationDictionary)
                    {
                        //Task.Run(() => {
                        if (item.Value.configurationType >= (ConfigurationType)7)
                        {
                            //Task.Run(() => { subMenuOnlyItems.Add(CreateConfigurationItemViewModel(provider, item.Key, item.Value)); });
                            subMenuOnlyItems.Add(CreateLinksViewModel(item.Value));
                        }
                        else
                        {
                            //Task.Run(() => { viewModels.Add(CreateConfigurationItemViewModel(provider, item.Key, item.Value)); });
                            viewModels.Add(CreateLinksViewModel(item.Value));
                        }
                    }
                    return viewModels;
                });
            });
            return host;
        }

        private static IHostBuilder AddProfiles(this IHostBuilder host)
        {
            List<Profiles> configurationDictionary = new List<Profiles>();

            DirectoryInfo profilesDirectory = new DirectoryInfo($"{Environment.GetEnvironmentVariable("windir")}\\AtlasModules\\Toolbox\\Profiles");
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
                ["StartMenuSubMenu"] = new("Start Menu", "Everything related to customizing the Windows Start Menu", ConfigurationType.Interface),
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
                        ObservableCollection<MultiOptionConfigurationItemViewModel> multiOptionItemViewModels = new();
                        ObservableCollection<LinksViewModel> linksViewModel = new();
                        foreach (ConfigurationItemViewModel configurationItemViewModel in subMenuOnlyItems.OfType<ConfigurationItemViewModel>())
                        {
                            if (configurationItemViewModel.Type.ToString() == item.Key)
                            {
                                itemViewModels.Add(configurationItemViewModel);
                            }
                        }
                        foreach (MultiOptionConfigurationItemViewModel configurationItemViewModel in subMenuOnlyItems.OfType<MultiOptionConfigurationItemViewModel>())
                        {
                            if (configurationItemViewModel.Type.ToString() == item.Key)
                            {
                                multiOptionItemViewModels.Add(configurationItemViewModel);
                            }
                        }
                        foreach (LinksViewModel configurationItemViewModel in subMenuOnlyItems.OfType<LinksViewModel>())
                        {
                            if (configurationItemViewModel.ConfigurationType.ToString() == item.Key)
                            {
                                linksViewModel.Add(configurationItemViewModel);
                            }
                        }
                        ConfigurationSubMenuViewModel viewModel = CreateConfigurationSubMenuViewModel(provider, itemViewModels, multiOptionItemViewModels, linksViewModel, item.Key, item.Value);
                        viewModels.Add(viewModel);
                    }
                    return viewModels;
                });
            });

            return host;
        }

        private static IHostBuilder AddMultiOptionConfigurationViewModels(this IHostBuilder host)
        {
            // TODO: Change configuration types
            Dictionary<string, MultiOptionConfiguration> configurationDictionary = new()
            {
                ["MultiOption"] = new("Multi option test configuration", "MultiOption", ConfigurationType.General, RiskRating.MediumRisk),
                ["ContextMenuTerminals"] = new("Add or remove terminals from the context menu", "ContextMenuTerminals", ConfigurationType.ContextMenuSubMenu, RiskRating.MediumRisk),
                ["ShortcutIcon"] = new("Change the icon from shortcuts", "ShortcutIcon", ConfigurationType.Interface, RiskRating.LowRisk),
            };

            host.ConfigureServices((_, services) =>
            {
                services.AddSingleton<IEnumerable<MultiOptionConfigurationItemViewModel>>(provider =>
                {
                    List<MultiOptionConfigurationItemViewModel> viewModels = new();

                    foreach (KeyValuePair<string, MultiOptionConfiguration> item in configurationDictionary)
                    {
                        if (
                        item.Value.Type >= (ConfigurationType)7)
                        {
                            subMenuOnlyItems.Add(CreateMultiOptionConfigurationItemViewModel(provider, item.Key, item.Value));
                        }
                        else
                        {
                            viewModels.Add(CreateMultiOptionConfigurationItemViewModel(provider, item.Key, item.Value));
                        }
                    }
                    return viewModels;
                });
            });
            return host;
        }

        private static IHostBuilder AddConfigurationItemViewModels(this IHostBuilder host)
        {
            // TODO: Change configuration types`
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
                //["Troubleshooting"] = new("Troubleshooting", "Troubleshooting", ConfigurationType.Troubleshooting, RiskRating.MediumRisk),
                ["CpuIdleContextMenu"] = new("CPU Idle toggle in context menu", "CpuIdleContextMenu", ConfigurationType.ContextMenuSubMenu, RiskRating.LowRisk),
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
                ["OldContextMenu"] = new("Legacy context menu (pre-Windows 11)", "OldContextMenu", ConfigurationType.ContextMenuSubMenu, RiskRating.MediumRisk),
                ["EdgeSwipe"] = new("Edge Swipe", "EdgeSwipe", ConfigurationType.Interface, RiskRating.LowRisk),
                ["AppIconsThumbnail"] = new("App icons on thumbnails", "AppIconsThumbnail", ConfigurationType.FileExplorerSubMenu, RiskRating.MediumRisk),
                ["AutomaticFolderDiscovery"] = new("Automatic folder discovery", "AutomaticFolderDiscovery", ConfigurationType.FileExplorerSubMenu, RiskRating.LowRisk),
                ["Gallery"] = new("Enable the gallery", "Gallery", ConfigurationType.FileExplorerSubMenu, RiskRating.LowRisk),
                ["SnapLayout"] = new("Enable snap layouts for windows", "SnapLayout", ConfigurationType.Interface, RiskRating.MediumRisk),
            };

            host.ConfigureServices((_,services) =>
            {
                services.AddSingleton<IEnumerable<ConfigurationItemViewModel>>(provider =>
                {
                    List<ConfigurationItemViewModel> viewModels = new();

                    foreach (KeyValuePair<string, Configuration> item in configurationDictionary)
                    {
                         //Task.Run(() => {
                            if (item.Value.Type >= (ConfigurationType)7)
                            {
                                //Task.Run(() => { subMenuOnlyItems.Add(CreateConfigurationItemViewModel(provider, item.Key, item.Value)); });
                                subMenuOnlyItems.Add(CreateConfigurationItemViewModel(provider, item.Key, item.Value));
                            }
                            else
                            {
                                //Task.Run(() => { viewModels.Add(CreateConfigurationItemViewModel(provider, item.Key, item.Value)); });
                                viewModels.Add(CreateConfigurationItemViewModel(provider, item.Key, item.Value));
                            }
                        //});

                    }
                    return viewModels;
                });
            });
            return host;
        }

        private static SoftwareItemViewModel CreateSoftwareItemViewModel(SoftwareItem softwareItem)
        {
            SoftwareItemViewModel viewModel = new(softwareItem);

            return viewModel;
        }

        private static LinksViewModel CreateLinksViewModel(Links linksItem)
        {
            LinksViewModel viewModel = new(linksItem);

            return viewModel;
        }

        private static MultiOptionConfigurationItemViewModel CreateMultiOptionConfigurationItemViewModel(
            IServiceProvider serviceProvider, object key, MultiOptionConfiguration configuration)
        {
            MultiOptionConfigurationItemViewModel viewModel = new(
                configuration, serviceProvider.GetRequiredKeyedService<MultiOptionConfigurationStore>(key), serviceProvider.GetRequiredKeyedService<IMultiOptionConfigurationServices>(key));

            return viewModel;
        }

        private static ConfigurationItemViewModel CreateConfigurationItemViewModel(
            IServiceProvider serviceProvider, object key, Configuration configuration)
        {
                ConfigurationItemViewModel viewModel = new(
                    configuration, serviceProvider.GetRequiredKeyedService<ConfigurationStore>(key), serviceProvider.GetRequiredKeyedService<IConfigurationService>(key));

                return viewModel;
        }

        #region Create ViewModels

        private static ConfigPageViewModel CreateConfigPageViewModel(IServiceProvider serviceProvider)
        {
            return ConfigPageViewModel.LoadViewModel(
                serviceProvider.GetServices<LinksViewModel>(),
                serviceProvider.GetServices<ConfigurationItemViewModel>(),
                serviceProvider.GetServices<MultiOptionConfigurationItemViewModel>(),
                serviceProvider.GetServices<ConfigurationSubMenuViewModel>());
        }

        private static HomePageViewModel CreateHomePageViewModel(IServiceProvider serviceProvider)
        {
            return HomePageViewModel.LoadViewModel(
                serviceProvider.GetServices<Profiles>(),
                serviceProvider.GetServices<ConfigurationItemViewModel>());
        }
        private static SoftwarePageViewModel CreateSoftwarePageViewModel(IServiceProvider serviceProvider)
        {
            return SoftwarePageViewModel.LoadViewModel(
                serviceProvider.GetServices<SoftwareItemViewModel>());
        }
        #endregion Create ViewModels

        private static ConfigurationSubMenuViewModel CreateConfigurationSubMenuViewModel(
          IServiceProvider serviceProvider, ObservableCollection<ConfigurationItemViewModel> configurationItemViewModels, ObservableCollection<MultiOptionConfigurationItemViewModel> multiOptionConfigurationItemViewModel, ObservableCollection<LinksViewModel> linksViewModel, object key, ConfigurationSubMenu configuration)
        {
            ConfigurationStoreSubMenu configurationStoreSubMenu = serviceProvider.GetRequiredKeyedService<ConfigurationStoreSubMenu>(key);

            ConfigurationSubMenuViewModel  viewModel = new(
               configuration, configurationStoreSubMenu, configurationItemViewModels, multiOptionConfigurationItemViewModel, linksViewModel);

            return viewModel;
        }
    }
}
