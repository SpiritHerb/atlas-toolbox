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
using AtlasToolbox.Commands;
using System.Windows.Input;
using Windows.Security.Cryptography.Core;
using Windows.Devices.WiFi;
using AtlasToolbox.Commands.ConfigurationButtonsCommand;
using AtlasToolbox.Utils;
using AtlasToolbox.Models.ProfileModels;
using Newtonsoft.Json;

namespace AtlasToolbox.HostBuilder
{
    public static class AddViewModelsHostBuilderExtensions
    {
        private static List<Object> subMenuOnlyItems = new List<Object>();
        private static Dictionary<string, string> list = new Dictionary<string, string>();
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {            
            host.ConfigureServices((_, services) =>
            {
                services.AddSingleton<MainViewModel>();
                services.AddTransient(CreateConfigPageViewModel);
                services.AddTransient(CreateHomePageViewModel);
                services.AddTransient(CreateSoftwarePageViewModel);
            });

            host.AddConfigurationButtonItemViewModels();
            host.AddLinksItemViewModels();
            host.AddSoftwareItemsViewModels();
            host.AddMultiOptionConfigurationViewModels();
            host.AddConfigurationItemViewModels();
            host.AddConfigurationSubMenu();
            host.AddProfiles();

            return host;
        }


        //private static string App.App.GetValueFromItemList(string key, bool desc = false)
        //{
        //    if (!desc) return list.Where(item => item.Key == key).Select(item => item.Value).FirstOrDefault();
        //    else return list.Where(item => item.Key == key + "Description").Select(item => item.Value).FirstOrDefault();
        //}

        /// <summary>
        /// Registers software items
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
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
                ["NVCleanstall"] = new("NVCleanstall", "TechPowerUp.NVCleanstall"),
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

        /// <summary>
        /// Regsiters profiles from the profile folder
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        private static IHostBuilder AddProfiles(this IHostBuilder host)
        {
            List<Profiles> configurationDictionary = new List<Profiles>();
            DirectoryInfo profilesDirectory = new DirectoryInfo($"{Environment.GetEnvironmentVariable("windir")}\\AtlasModules\\Toolbox\\Profiles");
            try
            {
                FileInfo[] profileFile = profilesDirectory.GetFiles();
            } catch
            {
                Directory.CreateDirectory($"{Environment.GetEnvironmentVariable("windir")}\\AtlasModules\\Toolbox\\Profiles");
            } finally
            {
                FileInfo[] profileFile = profilesDirectory.GetFiles();
                foreach (FileInfo file in profileFile)
                {
                    configurationDictionary.Add(ProfileSerializing.DeserializeProfile(file.FullName));
                };
                host.ConfigureServices((_, services) =>
                {
                    services.AddSingleton<IEnumerable<Profiles>> (provider =>
                    {
                        return configurationDictionary;
                    });
                });
            }

            return host;
        }

        /// <summary>
        /// Registers links
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        private static IHostBuilder AddLinksItemViewModels(this IHostBuilder host)
        {
            Dictionary<string, Links> configurationDictionary = new()
            {
                ["ExplorerPatcher"] = new ("https://github.com/valinet/ExplorerPatcher", "ExplorerPatcher", ConfigurationType.StartMenuSubMenu),
                ["StartAllBack"] = new ("https://www.startallback.com/", "StartAllBack", ConfigurationType.StartMenuSubMenu),
                ["OpenShellAtlasPreset"] = new (@"http://github.com/Atlas-OS/Atlas/blob/main/src/playbook/Executables/AtlasDesktop/4.%20Interface%20Tweaks/Start%20Menu/Atlas%20Open-Shell%20Preset.xml", App.GetValueFromItemList("OpenShellAtlasPreset"), ConfigurationType.StartMenuSubMenu),
                ["InterfaceTweaksDocumentation"] = new (@"https://docs.atlasos.net/getting-started/post-installation/atlas-folder/interface-tweaks/", App.GetValueFromItemList("InterfaceTweaksDocumentation"), ConfigurationType.Interface),
                
                ["ActivationPage"] = new (@"ms-settings:activation", App.GetValueFromItemList("ActivationPage"), ConfigurationType.Windows, "\uE713"),
                ["ColorsPage"] = new (@"ms-settings:personalization-colors", App.GetValueFromItemList("ColorsPage"), ConfigurationType.Windows, "\uE713"),
                ["DateAndTime"] = new (@"ms-settings:dateandtime", App.GetValueFromItemList("DateAndTime"), ConfigurationType.Windows, "\uE713"),
                ["DefaultApps"] = new (@"ms-settings:defaultapps", App.GetValueFromItemList("DefaultApps"), ConfigurationType.Windows, "\uE713"),
                ["DefaultGraphicsSettings"] = new (@"ms-settings:display-advancedgraphics-default", App.GetValueFromItemList("DefaultGraphicsSettings"), ConfigurationType.Windows, "\uE713"),
                ["RegionLanguage"] = new (@"ms-settings:regionlanguage", App.GetValueFromItemList("RegionLanguage"), ConfigurationType.Windows, "\uE713"),
                ["Privacy"] = new (@"ms-settings:privacy", App.GetValueFromItemList("Privacy"), ConfigurationType.Windows, "\uE713"),
                ["RegionProperties"] = new (@"ms-settings:regionProperties", App.GetValueFromItemList("RegionProperties"), ConfigurationType.Windows, "\uE713"),
                ["Taskbar"] = new (@"ms-settings:taskbar", App.GetValueFromItemList("Taskbar"), ConfigurationType.Windows, "\uE713"),
                ["CoreIsolation"] = new (@"windowsdefender://coreisolation/", App.GetValueFromItemList("CoreIsolation"), ConfigurationType.CoreIsolationSubMenu, "\uE83D"),


                ["WindowsSettingsDocumentation"] = new (@"https://docs.atlasos.net/getting-started/post-installation/atlas-folder/windows-settings/", App.GetValueFromItemList("WindowsSettingsDocumentation"), ConfigurationType.Windows),
                ["BootConfigExplanations"] = new (@"https://learn.microsoft.com/windows-hardware/drivers/devtest/bcdedit--set", App.GetValueFromItemList("BootConfigExplanations"), ConfigurationType.BootConfigurationSubMenu),
                ["AutoGpuAffinity"] = new (@"https://github.com/valleyofdoom/AutoGpuAffinity", "AutoGpuAffinity", ConfigurationType.DriverConfigurationSubMenu),
                ["GoInterruptPolicy"] = new (@"https://github.com/spddl/GoInterruptPolicy", "GoInterruptPolicy", ConfigurationType.DriverConfigurationSubMenu),
                ["InterrupAffinityTool"] = new (@"https://www.techpowerup.com/download/microsoft-interrupt-affinity-tool", App.GetValueFromItemList("InterrupAffinityTool"), ConfigurationType.DriverConfigurationSubMenu),
                ["MSIUtilityV3"] = new (@"https://forums.guru3d.com/threads/windows-line-based-vs-message-signaled-based-interrupts-msi-tool.378044", "MSI Utility V3", ConfigurationType.DriverConfigurationSubMenu),
                ["ProcessExplorerApp"] = new (@"https://learn.microsoft.com/en-us/sysinternals/downloads/process-explorer", App.GetValueFromItemList("ProcessExplorer"), ConfigurationType.Advanced),
                ["NvidiaDisplayContainerMustReadFirst"] = new (@"https://docs.atlasos.net/getting-started/post-installation/atlas-folder/advanced-configuration/#nvidia-display-container", App.GetValueFromItemList("NvidiaDisplayContainerMustReadFirst"), ConfigurationType.NvidiaDisplayContainerSubMenu),
                ["AdvancedConfigMustRead"] = new (@"https://docs.atlasos.net/getting-started/post-installation/atlas-folder/advanced-configuration/", App.GetValueFromItemList("AdvancedConfigMustRead"), ConfigurationType.Advanced),
                ["SecurityDocumentation"] = new (@"https://docs.atlasos.net/getting-started/post-installation/atlas-folder/security/", App.GetValueFromItemList("SecurityDocumentation"), ConfigurationType.Security),
                ["ResetPC"] = new (@"https://docs.atlasos.net/getting-started/reverting-atlas/", App.GetValueFromItemList("ResetPC"), ConfigurationType.Troubleshooting),
            };

            host.ConfigureServices((_, services) =>
            {
                services.AddSingleton<IEnumerable<LinksViewModel>>(provider =>
                {
                    List<LinksViewModel> viewModels = new();

                    foreach (KeyValuePair<string, Links> item in configurationDictionary)
                    {
                        viewModels.Add(CreateLinksViewModel(item.Value));
                    }
                    return viewModels;
                });
            });
            return host;
        }

        /// <summary>
        /// Registers configuration buttons
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        private static IHostBuilder AddConfigurationButtonItemViewModels(this IHostBuilder host)
        {
            ICommand buttonCommand;
            Dictionary<string, ConfigurationButton> configurationDictionary = new()
            {
                ["RestartExplorerButton"] = new(buttonCommand = new RestartExplorerCommand(), App.GetValueFromItemList("RestartExplorerButton"), App.GetValueFromItemList("RestartExplorerButton", true), ConfigurationType.Interface),
                ["ViewCurrentSettingsBootConfig"] = new(buttonCommand = new ViewCurrentValuesCommand(), App.GetValueFromItemList("ViewCurrentSettingsBootConfig"), App.GetValueFromItemList("ViewCurrentSettingsBootConfig", true), ConfigurationType.BootConfigurationSubMenu),
                ["VBSCurrentConfig"] = new(buttonCommand = new CurrentVBSConfigurationCommand(), App.GetValueFromItemList("VBSCurrentConfig"), App.GetValueFromItemList("VBSCurrentConfig", true), ConfigurationType.CoreIsolationSubMenu),
                ["ToggleDefender"] = new(buttonCommand = new ToggleDefenderCommand(), App.GetValueFromItemList("ToggleDefender"), App.GetValueFromItemList("ToggleDefender", true), ConfigurationType.DefenderSubMenu),
                ["ResetFTH"] = new(buttonCommand = new ResetFTHCommand(), App.GetValueFromItemList("ResetFTH"), App.GetValueFromItemList("ResetFTH", true), ConfigurationType.MitigationsSubMenu),
                ["InstallOpenShell"] = new(buttonCommand = new InstallOpenShellCommand(), App.GetValueFromItemList("InstallOpenShell"), App.GetValueFromItemList("InstallOpenShell", true), ConfigurationType.StartMenuSubMenu),

                ["FixErrors"] = new(buttonCommand = new FixErrorsCommand(), App.GetValueFromItemList("FixErrors"), App.GetValueFromItemList("FixErrors", true), ConfigurationType.Troubleshooting),
                ["RepairWinComponent"] = new(buttonCommand = new RepairWindowsComponentsCommand(), App.GetValueFromItemList("FixErrors"), App.GetValueFromItemList("RepairWinComponent"), ConfigurationType.Troubleshooting),
                ["TelemetryComponents"] = new(buttonCommand = new TelemetryComponentsCommand(), App.GetValueFromItemList("FixErrors"), App.GetValueFromItemList("TelemetryComponents"), ConfigurationType.Troubleshooting),
                ["AtlasDefault"] = new(buttonCommand = new NetworkAtlasDefaults(), App.GetValueFromItemList("ResetFTH"), App.GetValueFromItemList("AtlasDefault"), ConfigurationType.TroubleshootingNetwork),
                ["WindowsDefault"] = new(buttonCommand = new NetworkWindowsDefaults(), App.GetValueFromItemList("ResetFTH"), App.GetValueFromItemList("WindowsDefault"), ConfigurationType.TroubleshootingNetwork),
            };

            host.ConfigureServices((_, services) =>
            {
                services.AddSingleton<IEnumerable<ConfigurationButtonViewModel>>(provider =>
                {
                    List<ConfigurationButtonViewModel> viewModels = new();

                    foreach (KeyValuePair<string, ConfigurationButton> item in configurationDictionary)
                    {
                        viewModels.Add(CreateButtonViewModel(item.Value));
                    }
                    return viewModels;
                });
            });
            return host;
        }

        /// <summary>
        /// Registers sub-menus
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        private static IHostBuilder AddConfigurationSubMenu(this IHostBuilder host)
        {
            // TODO: Change configuration types
            Dictionary<string, ConfigurationSubMenu> configurationDictionary = new()
            {
                ["BootConfigAppearance"] = new(App.GetValueFromItemList("BootConfigAppearance"), App.GetValueFromItemList("BootConfigAppearance", true), ConfigurationType.BootConfigurationSubMenu),
                ["BootConfigBehavior"] = new(App.GetValueFromItemList("BootConfigBehavior"), App.GetValueFromItemList("BootConfigBehavior", true), ConfigurationType.BootConfigurationSubMenu),
                ["NvidiaDisplayContainerSubMenu"] = new(App.GetValueFromItemList("NvidiaDisplayContainerSubMenu"), App.GetValueFromItemList("NvidiaDisplayContainerSubMenu", true), ConfigurationType.ServicesSubMenu),

                ["StartMenuSubMenu"] = new(App.GetValueFromItemList("StartMenuSubMenu"), App.GetValueFromItemList("StartMenuSubMenu", true), ConfigurationType.Interface),
                ["ContextMenuSubMenu"] = new(App.GetValueFromItemList("ContextMenuSubMenu"), App.GetValueFromItemList("ContextMenuSubMenu", true), ConfigurationType.Interface),
                ["AiSubMenu"] = new(App.GetValueFromItemList("AiSubMenu"), App.GetValueFromItemList("AiSubMenu", true), ConfigurationType.General),
                ["ServicesSubMenu"] = new(App.GetValueFromItemList("ServicesSubMenu"), App.GetValueFromItemList("ServicesSubMenu", true), ConfigurationType.Advanced),
                ["BootConfigurationSubMenu"] = new(App.GetValueFromItemList("BootConfigurationSubMenu"), App.GetValueFromItemList("BootConfigurationSubMenu", true), ConfigurationType.Advanced),
                ["FileExplorerSubMenu"] = new(App.GetValueFromItemList("FileExplorerSubMenu"), App.GetValueFromItemList("FileExplorerSubMenu", true), ConfigurationType.Interface),
                ["DriverConfigurationSubMenu"] = new(App.GetValueFromItemList("DriverConfigurationSubMenu"), App.GetValueFromItemList("DriverConfigurationSubMenu", true), ConfigurationType.Advanced),
                ["CoreIsolationSubMenu"] = new( App.GetValueFromItemList("CoreIsolationSubMenu"), App.GetValueFromItemList("CoreIsolationSubMenu", true), ConfigurationType.Security),
                ["DefenderSubMenu"] = new(App.GetValueFromItemList("DefenderSubMenu"), App.GetValueFromItemList("DefenderSubMenu", true), ConfigurationType.Security),
                ["MitigationsSubMenu"] = new(App.GetValueFromItemList("MitigationsSubMenu"), App.GetValueFromItemList("MitigationsSubMenu", true), ConfigurationType.Security),
                ["TroubleshootingNetwork"] = new(App.GetValueFromItemList("TroubleshootingNetwork"), App.GetValueFromItemList("TroubleshootingNetwork", true), ConfigurationType.Troubleshooting),
                ["FileSharingSubMenu"] = new(App.GetValueFromItemList("FileSharingSubMenu"), App.GetValueFromItemList("FileSharingSubMenu", true), ConfigurationType.General),
                ["WindowsUpdate"] = new(App.GetValueFromItemList("WindowsUpdate"), App.GetValueFromItemList("WindowsUpdate", true), ConfigurationType.General),
            };
            host.ConfigureServices((_, services) =>
            {
                services.AddSingleton<IEnumerable<ConfigurationSubMenuViewModel>>(provider =>
                {
                    List<ConfigurationSubMenuViewModel> viewModels = new();
                    foreach (KeyValuePair<string, ConfigurationSubMenu> subMenu in configurationDictionary)
                    {
                        ObservableCollection<ConfigurationItemViewModel> itemViewModels = new ObservableCollection<ConfigurationItemViewModel>(provider.GetServices<ConfigurationItemViewModel>().Where(item => item.Type.ToString() == subMenu.Key));
                        ObservableCollection<MultiOptionConfigurationItemViewModel> multiOptionItemViewModels = new ObservableCollection<MultiOptionConfigurationItemViewModel>(provider.GetServices<MultiOptionConfigurationItemViewModel>().Where(item => item.Type.ToString() == subMenu.Key));
                        ObservableCollection<LinksViewModel> linksViewModel = new ObservableCollection<LinksViewModel>(provider.GetServices<LinksViewModel>().Where(item => item.Type.ToString() == subMenu.Key));
                        ObservableCollection<ConfigurationSubMenuViewModel> configurationSubMenuViewModels = new ObservableCollection<ConfigurationSubMenuViewModel>(viewModels.Where(item => item.Type.ToString() == subMenu.Key));
                        ObservableCollection<ConfigurationButtonViewModel> configurationButtonViewModels = new ObservableCollection<ConfigurationButtonViewModel>(provider.GetServices<ConfigurationButtonViewModel>().Where(item => item.Type.ToString() == subMenu.Key));

                        ConfigurationSubMenuViewModel viewModel = CreateConfigurationSubMenuViewModel(provider, itemViewModels, multiOptionItemViewModels, linksViewModel, subMenu.Key, subMenu.Value, configurationSubMenuViewModels, configurationButtonViewModels);
                        viewModels.Add(viewModel);
                    }
                    return viewModels;
                });
            });

            return host;
        }


        /// <summary>
        /// Registers multioption configuration services
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        private static IHostBuilder AddMultiOptionConfigurationViewModels(this IHostBuilder host)
        {
            // TODO: Change configuration types
            Dictionary<string, MultiOptionConfiguration> configurationDictionary = new()
            {
                ["ContextMenuTerminals"] = new(App.GetValueFromItemList("ContextMenuTerminals"), "ContextMenuTerminals", ConfigurationType.ContextMenuSubMenu, RiskRating.MediumRisk),
                ["ShortcutIcon"] = new(App.GetValueFromItemList("ShortcutIcon"), "ShortcutIcon", ConfigurationType.Interface, RiskRating.LowRisk),
                ["Mitigations"] = new(App.GetValueFromItemList("Mitigations"), "Mitigations", ConfigurationType.MitigationsSubMenu, RiskRating.MediumRisk),
                ["SafeMode"] = new(App.GetValueFromItemList("SafeMode"), "SafeMode", ConfigurationType.Troubleshooting, RiskRating.MediumRisk),
            };

            host.ConfigureServices((_, services) =>
            {
                services.AddSingleton<IEnumerable<MultiOptionConfigurationItemViewModel>>(provider =>
                {
                    List<MultiOptionConfigurationItemViewModel> viewModels = new();

                    foreach (KeyValuePair<string, MultiOptionConfiguration> item in configurationDictionary)
                    {
                        viewModels.Add(CreateMultiOptionConfigurationItemViewModel(provider, item.Key, item.Value));
                    }
                    return viewModels;
                });
            });
            return host;
        }

        /// <summary>
        /// Registers configuration items
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        private static IHostBuilder AddConfigurationItemViewModels(this IHostBuilder host)
        {
            // TODO: Change configuration types`
            Dictionary<string, Configuration> configurationDictionary = new()
            {
                ["Animations"] = new (App.GetValueFromItemList("Animations"), "Animations", ConfigurationType.Interface, RiskRating.LowRisk),
                ["ExtractContextMenu"] = new(App.GetValueFromItemList("ExtractContextMenu"), "ExtractContextMenu", ConfigurationType.ContextMenuSubMenu, RiskRating.LowRisk),
                ["RunWithPriority"] = new(App.GetValueFromItemList("RunWithPriority"), "RunWithPriority", ConfigurationType.ContextMenuSubMenu, RiskRating.MediumRisk),
                ["Bluetooth"] = new("Bluetooth", "Bluetooth", ConfigurationType.ServicesSubMenu, RiskRating.HighRisk),
                ["LanmanWorkstation"] = new(App.GetValueFromItemList("LanmanWorkstation"), "LanmanWorkstation", ConfigurationType.ServicesSubMenu, RiskRating.HighRisk),
                ["NetworkDiscovery"] = new(App.GetValueFromItemList("NetworkDiscovery"), "NetworkDiscovery", ConfigurationType.ServicesSubMenu, RiskRating.HighRisk),
                ["Printing"] = new(App.GetValueFromItemList("Printing"), "Printing", ConfigurationType.ServicesSubMenu, RiskRating.LowRisk),
                ["NvidiaDispayContainer"] = new(App.GetValueFromItemList("NvidiaDispayContainer"), "NvidiaDispayContainer", ConfigurationType.NvidiaDisplayContainerSubMenu, RiskRating.HighRisk),
                ["AddNvidiaDisplayContainerContextMenu"] = new(App.GetValueFromItemList("AddNvidiaDisplayContainerContextMenu"), "AddNvidiaDisplayContainerContextMenu", ConfigurationType.NvidiaDisplayContainerSubMenu, RiskRating.HighRisk),
                ["CpuIdleContextMenu"] = new(App.GetValueFromItemList("CpuIdleContextMenu"), "CpuIdleContextMenu", ConfigurationType.ContextMenuSubMenu, RiskRating.MediumRisk),
                ["LockScreen"] = new(App.GetValueFromItemList("LockScreen"), "LockScreen", ConfigurationType.Interface, RiskRating.LowRisk),
                ["ShortcutText"] = new(App.GetValueFromItemList("ShortcutText"), "ShortcutText", ConfigurationType.Interface, RiskRating.LowRisk),
                ["BootLogo"] = new(App.GetValueFromItemList("BootLogo"), "BootLogo", ConfigurationType.BootConfigAppearance, RiskRating.LowRisk),
                ["BootMessages"] = new(App.GetValueFromItemList("BootMessages"), "BootMessages", ConfigurationType.BootConfigAppearance, RiskRating.LowRisk),
                ["NewBootMenu"] = new(App.GetValueFromItemList("NewBootMenu"), "NewBootMenu", ConfigurationType.BootConfigAppearance, RiskRating.LowRisk),
                ["SpinningAnimation"] = new(App.GetValueFromItemList("SpinningAnimation"), "SpinningAnimations", ConfigurationType.BootConfigAppearance, RiskRating.LowRisk),
                ["AdvancedBootOptions"] = new(App.GetValueFromItemList("AdvancedBootOptions"), "AdvancedBootOptions", ConfigurationType.BootConfigBehavior, RiskRating.MediumRisk),
                ["AutomaticRepair"] = new(App.GetValueFromItemList("AutomaticRepair"), "AutomaticRepair", ConfigurationType.BootConfigBehavior, RiskRating.MediumRisk),
                ["KernelParameters"] = new(App.GetValueFromItemList("KernelParameters"), "KernelParameters", ConfigurationType.BootConfigBehavior, RiskRating.LowRisk),
                ["HighestMode"] = new(App.GetValueFromItemList("HighestMode"), "HighestMode", ConfigurationType.BootConfigBehavior, RiskRating.LowRisk),
                ["CompactView"] = new(App.GetValueFromItemList("CompactView"), "CompactView", ConfigurationType.FileExplorerSubMenu, RiskRating.LowRisk),
                ["RemovableDrivesInSidebar"] = new(App.GetValueFromItemList("RemovableDrivesInSidebar"), "RemovableDrivesInSidebar", ConfigurationType.FileExplorerSubMenu, RiskRating.MediumRisk),
                ["BackgroundApps"] = new(App.GetValueFromItemList("BackgroundApps"), "BackgroundApps", ConfigurationType.General, RiskRating.MediumRisk),
                ["SearchIndexing"] = new(App.GetValueFromItemList("SearchIndexing"), "SearchIndexing", ConfigurationType.General, RiskRating.MediumRisk),
                ["FsoAndGameBar"] = new(App.GetValueFromItemList("FsoAndGameBar"), "FsoAndGameBar", ConfigurationType.General, RiskRating.LowRisk),
                ["AutomaticUpdates"] = new(App.GetValueFromItemList("AutomaticUpdates"), "AutomaticUpdates", ConfigurationType.General, RiskRating.HighRisk),
                ["DeliveryOptimisation"] = new(App.GetValueFromItemList("DeliveryOptimisation"), "DeliveryOptimisation", ConfigurationType.General, RiskRating.LowRisk),
                ["Hibernation"] = new(App.GetValueFromItemList("Hibernation"), "Hibernation", ConfigurationType.General, RiskRating.LowRisk),
                ["Location"] = new(App.GetValueFromItemList("Location"), "Location", ConfigurationType.General, RiskRating.LowRisk),
                ["PhoneLink"] = new(App.GetValueFromItemList("PhoneLink"), "PhoneLink", ConfigurationType.General, RiskRating.LowRisk),
                ["PowerSaving"] = new(App.GetValueFromItemList("PowerSaving"), "PowerSaving", ConfigurationType.General, RiskRating.LowRisk),
                ["Sleep"] = new(App.GetValueFromItemList("Sleep"), "Sleep", ConfigurationType.General, RiskRating.LowRisk),
                ["SystemRestore"] = new(App.GetValueFromItemList("SystemRestore"), "SystemRestore", ConfigurationType.General, RiskRating.HighRisk),
                ["UpdateNotifications"] = new(App.GetValueFromItemList("UpdateNotifications"), "UpdateNotifications", ConfigurationType.General, RiskRating.LowRisk),
                ["WebSearch"] = new(App.GetValueFromItemList("WebSearch"), "WebSearch", ConfigurationType.General, RiskRating.HighRisk),
                ["Widgets"] = new(App.GetValueFromItemList("Widgets"), "Widgets", ConfigurationType.General, RiskRating.LowRisk),
                ["WindowsSpotlight"] = new(App.GetValueFromItemList("WindowsSpotlight"), "WindowsSpotlight", ConfigurationType.General, RiskRating.HighRisk),
                ["AppStoreArchiving"] = new(App.GetValueFromItemList("AppStoreArchiving"), "AppStoreArchiving", ConfigurationType.General, RiskRating.HighRisk),
                ["TakeOwnership"] = new(App.GetValueFromItemList("TakeOwnership"), "TakeOwnership", ConfigurationType.ContextMenuSubMenu, RiskRating.HighRisk),
                ["OldContextMenu"] = new(App.GetValueFromItemList("OldContextMenu"), "OldContextMenu", ConfigurationType.ContextMenuSubMenu, RiskRating.MediumRisk),
                ["EdgeSwipe"] = new(App.GetValueFromItemList("EdgeSwipe"), "EdgeSwipe", ConfigurationType.Interface, RiskRating.LowRisk),
                ["AppIconsThumbnail"] = new(App.GetValueFromItemList("AppIconsThumbnail"), "AppIconsThumbnail", ConfigurationType.FileExplorerSubMenu, RiskRating.MediumRisk),
                ["AutomaticFolderDiscovery"] = new(App.GetValueFromItemList("AutomaticFolderDiscovery"), "AutomaticFolderDiscovery", ConfigurationType.FileExplorerSubMenu, RiskRating.LowRisk),
                ["Gallery"] = new(App.GetValueFromItemList("Gallery"), "Gallery", ConfigurationType.FileExplorerSubMenu, RiskRating.LowRisk),
                ["SnapLayout"] = new(App.GetValueFromItemList("SnapLayout"), "SnapLayout", ConfigurationType.Interface, RiskRating.LowRisk),
                ["RecentItems"] = new(App.GetValueFromItemList("RecentItems"), "RecentItems", ConfigurationType.Interface, RiskRating.LowRisk),
                ["VerboseStatusMessage"] = new(App.GetValueFromItemList("VerboseStatusMessage"), "VerboseStatusMessage", ConfigurationType.Interface, RiskRating.LowRisk),
                ["SuperFetch"] = new(App.GetValueFromItemList("SuperFetch"), "SuperFetch", ConfigurationType.ServicesSubMenu, RiskRating.HighRisk),
                ["StaticIp"] = new(App.GetValueFromItemList("StaticIp"), "StaticIp", ConfigurationType.Advanced, RiskRating.MediumRisk),
                ["HideAppBrowserControl"] = new(App.GetValueFromItemList("HideAppBrowserControl"), "HideAppBrowserControl", ConfigurationType.DefenderSubMenu, RiskRating.HighRisk),
                ["SecurityHealthTray"] = new(App.GetValueFromItemList("SecurityHealthTray"), "SecurityHealthTray", ConfigurationType.DefenderSubMenu, RiskRating.MediumRisk),
                ["FaultTolerantHeap"] = new(App.GetValueFromItemList("FaultTolerantHeap"), "FaultTolerantHeap", ConfigurationType.MitigationsSubMenu, RiskRating.MediumRisk),
                ["Copilot"] = new(App.GetValueFromItemList("Copilot"), "Copilot", ConfigurationType.AiSubMenu, RiskRating.HighRisk),
                ["Recall"] = new(App.GetValueFromItemList("Recall"), "recall", ConfigurationType.AiSubMenu, RiskRating.HighRisk),
                ["CpuIdle"] = new(App.GetValueFromItemList("CpuIdle"), "CpuIdle", ConfigurationType.General, RiskRating.HighRisk),
                ["ProcessExplorer"] = new(App.GetValueFromItemList("ProcessExplorer"), "ProcessExplorer", ConfigurationType.Advanced, RiskRating.MediumRisk),
                ["VbsState"] = new(App.GetValueFromItemList("VbsState"), "VbsState", ConfigurationType.CoreIsolationSubMenu, RiskRating.HighRisk),
                ["GiveAccessToMenu"] = new(App.GetValueFromItemList("GiveAccessToMenu"), "GiveAccessToMenu", ConfigurationType.FileSharingSubMenu, RiskRating.HighRisk),
                ["NetworkNavigationPane"] = new(App.GetValueFromItemList("NetworkNavigationPane"), "NetworkNavigationPane", ConfigurationType.FileSharingSubMenu, RiskRating.HighRisk),
                ["FileSharing"] = new(App.GetValueFromItemList("FileSharing"), "FileSharing", ConfigurationType.FileSharingSubMenu, RiskRating.HighRisk),
                ["WindowsHello"] = new(App.GetValueFromItemList("WindowsHello"), "WindowsHello", ConfigurationType.General, RiskRating.HighRisk),
                ["ToggleWindowsUpdates"] = new(App.GetValueFromItemList("ToggleWindowsUpdates"), "ToggleWindowsUpdates", ConfigurationType.WindowsUpdate, RiskRating.HighRisk),
            };

            host.ConfigureServices((_,services) =>
            {
                services.AddSingleton<IEnumerable<ConfigurationItemViewModel>>(provider =>
                {
                List<ConfigurationItemViewModel> viewModels = new();

                foreach (KeyValuePair<string, Configuration> item in configurationDictionary)
                {
                    //Could work, but needs to await for everything to be completed before returning viewModels
                    //Task.Run(() => { viewModels.Add(CreateConfigurationItemViewModel(provider, item.Key, item.Value)); });
                    viewModels.Add(CreateConfigurationItemViewModel(provider, item.Key, item.Value));
                }
                    return viewModels;
                });
            });
            return host;
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
        // Entire region is made to create view models
        private static SoftwareItemViewModel CreateSoftwareItemViewModel(SoftwareItem softwareItem)
        {
            SoftwareItemViewModel viewModel = new(softwareItem);

            return viewModel;
        }

        private static ConfigurationButtonViewModel CreateButtonViewModel(ConfigurationButton configurationButtonViewModel)
        {
            ConfigurationButtonViewModel viewModel = new(configurationButtonViewModel);

            return viewModel;
        }

        private static LinksViewModel CreateLinksViewModel(Links linksItem)
        {
            LinksViewModel viewModel = new(linksItem);

            return viewModel;
        }

        private static ConfigPageViewModel CreateConfigPageViewModel(IServiceProvider serviceProvider)
        {
            return ConfigPageViewModel.LoadViewModel(
                serviceProvider.GetServices<LinksViewModel>(),
                serviceProvider.GetServices<ConfigurationItemViewModel>(),
                serviceProvider.GetServices<MultiOptionConfigurationItemViewModel>(),
                serviceProvider.GetServices<ConfigurationSubMenuViewModel>(),
                serviceProvider.GetServices<ConfigurationButtonViewModel>());
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
        private static ConfigurationSubMenuViewModel CreateConfigurationSubMenuViewModel(
          IServiceProvider serviceProvider, ObservableCollection<ConfigurationItemViewModel> configurationItemViewModels, ObservableCollection<MultiOptionConfigurationItemViewModel> multiOptionConfigurationItemViewModel, ObservableCollection<LinksViewModel> linksViewModel, object key, ConfigurationSubMenu configuration, ObservableCollection<ConfigurationSubMenuViewModel> configurationSubMenuViewModel, ObservableCollection<ConfigurationButtonViewModel> configurationButtonViewModels)
        {
            ConfigurationStoreSubMenu configurationStoreSubMenu = serviceProvider.GetRequiredKeyedService<ConfigurationStoreSubMenu>(key);

            ConfigurationSubMenuViewModel  viewModel = new(
               configuration, configurationStoreSubMenu, configurationItemViewModels, multiOptionConfigurationItemViewModel, linksViewModel, configurationSubMenuViewModel, configurationButtonViewModels);

            return viewModel;
        }
        #endregion Create ViewModels
    }
}
