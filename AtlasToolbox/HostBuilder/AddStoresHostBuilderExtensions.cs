using AtlasToolbox.Services.ConfigurationServices;
using AtlasToolbox.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVMEssentials.Stores;

namespace AtlasToolbox.HostBuilder
{
    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            host.ConfigureServices((_,services) =>
            {
            });

            host.AddConfigurationStores();
            host.AddConfigurationMenu();

            return host;
        }

        private static IHostBuilder AddConfigurationStores(this IHostBuilder host)
        {
            host.ConfigureServices((_, services) =>
            {
                services.AddKeyedSingleton<ConfigurationStore>("Animations");
                services.AddKeyedSingleton<ConfigurationStore>("Bluetooth");
                services.AddKeyedSingleton<ConfigurationStore>("FsoAndGameBar");
                services.AddKeyedSingleton<ConfigurationStore>("WindowsFirewall");
                services.AddKeyedSingleton<ConfigurationStore>("GameMode");
                services.AddKeyedSingleton<ConfigurationStore>("Hags");
                services.AddKeyedSingleton<ConfigurationStore>("LanmanWorkstation");
                services.AddKeyedSingleton<ConfigurationStore>("MicrosoftStore");
                services.AddKeyedSingleton<ConfigurationStore>("NetworkDiscovery");
                services.AddKeyedSingleton<ConfigurationStore>("Notifications");
                services.AddKeyedSingleton<ConfigurationStore>("Printing");
                services.AddKeyedSingleton<ConfigurationStore>("SearchIndexing");
                services.AddKeyedSingleton<ConfigurationStore>("Troubleshooting");
                services.AddKeyedSingleton<ConfigurationStore>("Uwp");
                services.AddKeyedSingleton<ConfigurationStore>("Vpn");
                services.AddKeyedSingleton<ConfigurationStore>("ModernAltTab");
                services.AddKeyedSingleton<ConfigurationStore>("CpuIdleContextMenu");
                services.AddKeyedSingleton<ConfigurationStore>("DarkTitlebars");
                services.AddKeyedSingleton<ConfigurationStore>("LockScreen");
                services.AddKeyedSingleton<ConfigurationStore>("ModernVolumeFlyout");
                services.AddKeyedSingleton<ConfigurationStore>("RunWithPriorityContextMenu");
                services.AddKeyedSingleton<ConfigurationStore>("ShortcutText");
                services.AddKeyedSingleton<ConfigurationStore>("BootLogo");
                services.AddKeyedSingleton<ConfigurationStore>("BootMessages");
                services.AddKeyedSingleton<ConfigurationStore>("NewBootMenu");
                services.AddKeyedSingleton<ConfigurationStore>("SpinningAnimation");
                services.AddKeyedSingleton<ConfigurationStore>("AdvancedBootOptions");
                services.AddKeyedSingleton<ConfigurationStore>("AutomaticRepair");
                services.AddKeyedSingleton<ConfigurationStore>("KernelParameters");
                services.AddKeyedSingleton<ConfigurationStore>("HighestMode");
                services.AddKeyedSingleton<ConfigurationStore>("CompactView");
                services.AddKeyedSingleton<ConfigurationStore>("QuickAccess");
                services.AddKeyedSingleton<ConfigurationStore>("RemovableDrivesInSidebar");
                services.AddKeyedSingleton<ConfigurationStore>("AutomaticUpdates");
                services.AddKeyedSingleton<ConfigurationStore>("BackgroundApps");
                services.AddKeyedSingleton<ConfigurationStore>("DeliveryOptimization");
                services.AddKeyedSingleton<ConfigurationStore>("Hibernation");
                services.AddKeyedSingleton<ConfigurationStore>("Location");
                services.AddKeyedSingleton<ConfigurationStore>("PhoneLink");
                services.AddKeyedSingleton<ConfigurationStore>("PowerSaving");
                services.AddKeyedSingleton<ConfigurationStore>("Sleep");
                services.AddKeyedSingleton<ConfigurationStore>("AppStoreArchiving");
                services.AddKeyedSingleton<ConfigurationStore>("SystemRestore");
                services.AddKeyedSingleton<ConfigurationStore>("UpdateNotifications");
                services.AddKeyedSingleton<ConfigurationStore>("WebSearch");
                services.AddKeyedSingleton<ConfigurationStore>("Widgets");
                services.AddKeyedSingleton<ConfigurationStore>("WindowsSpotlight");
                services.AddKeyedSingleton<ConfigurationStore>("ExtractContextMenu");
                services.AddKeyedSingleton<ConfigurationStore>("AppStoreArchiving");
            });

            return host;
        }

        private static IHostBuilder AddConfigurationMenu(this IHostBuilder host)
        {
            host.ConfigureServices((_, services) =>
            {
                services.AddKeyedSingleton<ConfigurationMenu>("ContextMenu");
            });

            return host;
        }
    }
}
