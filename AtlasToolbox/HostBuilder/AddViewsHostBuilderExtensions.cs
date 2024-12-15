using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVMEssentials.ViewModels;

namespace AtlasOS_Toolbox.HostBuilders
{
    public static class AddViewsHostBuilderExtensions
    {
        public static IHostBuilder AddViews(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton((services) => new MainWindow()
                {
                    DataContext = services.GetRequiredService<MainViewModel>()
                });
            });

            return host;
        }
    }
}
