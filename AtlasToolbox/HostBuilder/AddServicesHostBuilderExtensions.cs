using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Services;
using AtlasToolbox.Services.ConfigurationSubMenu;
using AtlasToolbox.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVMEssentials.Services;
using MVVMEssentials.Stores;
using System;
using AtlasOSToolbox.Services.ConfigurationServices;
using BcdSharp;

namespace AtlasToolbox.HostBuilder
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices((_,services) =>
            {
                services.AddTransient(CreateBcdStore);
                services.AddTransient<IDismService, DismService>();
                services.AddTransient<IBcdService, BcdService>();
            });

            host.AddConfigurationServices();
            host.AddConfigurationMenus();

            return host;
        }

        private static BcdStore CreateBcdStore(IServiceProvider _)
        {
            return BcdStore.OpenStore();
        }

        private static IHostBuilder AddConfigurationServices(this IHostBuilder host)
        {
            host.ConfigureServices((_, services) =>
            {
                services.AddKeyedTransient<IConfigurationService, AnimationsConfigurationService>("Animations");
                services.AddKeyedTransient<IConfigurationService, AppStoreArchivingConfigurationService>("AppStoreArchiving");
                services.AddKeyedTransient<IConfigurationService, BluetoothConfigurationService>("Bluetooth");
                services.AddKeyedTransient<IConfigurationService, FsoAndGameBarConfigurationService>("FsoAndGameBar");
                services.AddKeyedTransient<IConfigurationService, WindowsFirewallConfigurationService>("WindowsFirewall");
                services.AddKeyedTransient<IConfigurationService, GameModeConfigurationService>("GameMode");
                services.AddKeyedTransient<IConfigurationService, HagsConfigurationService>("Hags");
                services.AddKeyedTransient<IConfigurationService, LanmanWorkstationConfigurationService>("LanmanWorkstation");
                services.AddKeyedTransient<IConfigurationService, MicrosoftStoreConfigurationService>("MicrosoftStore");
                services.AddKeyedTransient<IConfigurationService, NetworkDiscoveryConfigurationService>("NetworkDiscovery");
                services.AddKeyedTransient<IConfigurationService, NotificationsConfigurationService>("Notifications");
                services.AddKeyedTransient<IConfigurationService, PrintingConfigurationService>("Printing");
                services.AddKeyedTransient<IConfigurationService, SearchIndexingConfigurationService>("SearchIndexing");
                services.AddKeyedTransient<IConfigurationService, TroubleshootingConfigurationService>("Troubleshooting");
                services.AddKeyedTransient<IConfigurationService, UwpConfigurationService>("Uwp");
                services.AddKeyedTransient<IConfigurationService, VpnConfigurationService>("Vpn");
                services.AddKeyedTransient<IConfigurationService, ModernAltTabConfigurationService>("ModernAltTab");
                services.AddKeyedTransient<IConfigurationService, CpuIdleContextMenuConfigurationService>("CpuIdleContextMenu");
                services.AddKeyedTransient<IConfigurationService, DarkTitlebarsConfigurationService>("DarkTitlebars");
                services.AddKeyedTransient<IConfigurationService, LockScreenConfigurationService>("LockScreen");
                services.AddKeyedTransient<IConfigurationService, ModernVolumeFlyoutConfigurationService>("ModernVolumeFlyout");
                services.AddKeyedTransient<IConfigurationService, RunWithPriorityContextMenuConfigurationService>("RunWithPriorityContextMenu");
                services.AddKeyedTransient<IConfigurationService, ShortcutTextConfigurationService>("ShortcutText");
                services.AddKeyedTransient<IConfigurationService, BootLogoConfigurationService>("BootLogo");
                services.AddKeyedTransient<IConfigurationService, BootMessagesConfigurationService>("BootMessages");
                services.AddKeyedTransient<IConfigurationService, NewBootMenuConfigurationService>("NewBootMenu");
                services.AddKeyedTransient<IConfigurationService, SpinningAnimationConfigurationService>("SpinningAnimation");
                services.AddKeyedTransient<IConfigurationService, AdvancedBootOptionsConfigurationService>("AdvancedBootOptions");
                services.AddKeyedTransient<IConfigurationService, AutomaticRepairConfigurationService>("AutomaticRepair");
                services.AddKeyedTransient<IConfigurationService, KernelParametersConfigurationService>("KernelParameters");
                services.AddKeyedTransient<IConfigurationService, HighestModeConfigurationService>("HighestMode");
                services.AddKeyedTransient<IConfigurationService, CompactViewConfigurationService>("CompactView");
                services.AddKeyedTransient<IConfigurationService, QuickAccessConfigurationService>("QuickAccess");
                services.AddKeyedTransient<IConfigurationService, RemovableDrivesInSidebarConfigurationService>("RemovableDrivesInSidebar");
                services.AddKeyedTransient<IConfigurationService, AutomaticUpdatesConfigurationService>("AutomaticUpdates");
                services.AddKeyedTransient<IConfigurationService, BackgroundAppsConfigurationService>("BackgroundApps");
                services.AddKeyedTransient<IConfigurationService, DeliveryOptimisationConfigurationService>("DeliveryOptimisation");
                services.AddKeyedTransient<IConfigurationService, HibernationConfigurationService>("Hibernation");
                services.AddKeyedTransient<IConfigurationService, LocationConfigurationService>("Location");
                services.AddKeyedTransient<IConfigurationService, PhoneLinkConfigurationService>("PhoneLink");
                services.AddKeyedTransient<IConfigurationService, PowerSavingConfigurationService>("PowerSaving");
                services.AddKeyedTransient<IConfigurationService, SleepConfigurationService>("Sleep");
                services.AddKeyedTransient<IConfigurationService, AppStoreArchivingConfigurationService>("AppStoreArchiving");
                services.AddKeyedTransient<IConfigurationService, SystemRestoreConfigurationService>("SystemRestore");
                services.AddKeyedTransient<IConfigurationService, UpdateNotificationsConfigurationService>("UpdateNotifications");
                services.AddKeyedTransient<IConfigurationService, WebSearchConfigurationService>("WebSearch");
                services.AddKeyedTransient<IConfigurationService, WidgetsConfigurationService>("Widgets");
                services.AddKeyedTransient<IConfigurationService, WindowsSpotlightConfigurationService>("WindowsSpotlight");
                services.AddKeyedTransient<IConfigurationService, ExtractContextMenuConfigurationService>("ExtractContextMenu");
                services.AddKeyedTransient<IConfigurationService, RunWithPriorityContextMenuConfigurationService>("RunWithPriority");
                services.AddKeyedTransient<IConfigurationService, TakeOwnershipConfigurationService>("TakeOwnership");
                services.AddKeyedTransient<IConfigurationService, TestConfigurationService>("TestConfig");
                services.AddKeyedTransient<IConfigurationService, OtherTestConfigurationService>("OtherTestConfig");
                services.AddKeyedTransient<IMultiOptionConfigurationServices, MultiOptionTestConfigurationService>("MultiOption");
                services.AddKeyedTransient<IMultiOptionConfigurationServices, ContextMenuTeminalsConfigurationService>("ContextMenuTerminals");
                services.AddKeyedTransient<IConfigurationService, OldContextMenuConfigurationService>("OldContext");
            });

            return host;
        }

        private static IHostBuilder AddConfigurationMenus(this IHostBuilder host)
        {
            host.ConfigureServices((_,services) =>
            {
                services.AddKeyedTransient<IConfigurationSubMenu, ContextMenuSubMenu>("ContextMenu");
                services.AddKeyedTransient<IConfigurationSubMenu, AiSubMenu>("AiSubMenu");
                services.AddKeyedTransient<IConfigurationSubMenu, CPUIdleSubMenu>("CPUIdleSubMenu");
                services.AddKeyedTransient<IConfigurationSubMenu, ServicesSubMenu>("ServicesSubMenu");
                services.AddKeyedTransient<IConfigurationSubMenu, BootConfigurationSubMenu>("BootConfigurationSubMenu");
                services.AddKeyedTransient<IConfigurationSubMenu, FileExplorerSubMenu>("FileExplorerSubMenu");
            });

            return host;
        }
    }
}
