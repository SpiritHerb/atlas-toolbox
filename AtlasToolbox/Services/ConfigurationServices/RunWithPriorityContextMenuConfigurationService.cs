using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class RunWithPriorityContextMenuConfigurationService : IConfigurationService
    {
        private const string PRIORITY_KEY_NAME = @"HKCR\exefile\shell\Priority";

        private readonly ConfigurationStore _runWithPriorityContextMenuConfigurationStore;

        public RunWithPriorityContextMenuConfigurationService(
            [FromKeyedServices("RunWithPriority")] ConfigurationStore runWithPriorityContextMenuConfigurationStore)
        {
            _runWithPriorityContextMenuConfigurationStore = runWithPriorityContextMenuConfigurationStore;
        }

        public void Disable()
        {
            RegistryHelper.DeleteKey(PRIORITY_KEY_NAME);

            _runWithPriorityContextMenuConfigurationStore.CurrentSetting = IsEnabled();
        }

        public void Enable()
        {
            RegistryHelper.SetValue(PRIORITY_KEY_NAME, "MUIVerb", "Run with priority");
            RegistryHelper.SetValue(PRIORITY_KEY_NAME, "SubCommands", string.Empty);

            string realtimeFlyoutKeyName = Path.Combine(PRIORITY_KEY_NAME, "shell", "001flyout");
            RegistryHelper.SetValue(realtimeFlyoutKeyName, null, "Realtime");

            string realtimeFlyoutCommandKeyName = Path.Combine(realtimeFlyoutKeyName, "command");
            RegistryHelper.SetValue(realtimeFlyoutCommandKeyName, null, "cmd /c start \"\" /Realtime \"%1\"");

            string highFlyoutKeyName = Path.Combine(PRIORITY_KEY_NAME, "shell", "002flyout");
            RegistryHelper.SetValue(highFlyoutKeyName, null, "High");

            string highFlyoutCommandKeyName = Path.Combine(highFlyoutKeyName, "command");
            RegistryHelper.SetValue(highFlyoutCommandKeyName, null, "cmd /c start \"\" /High \"%1\"");

            string normalFlyoutKeyName = Path.Combine(PRIORITY_KEY_NAME, "shell", "003flyout");
            RegistryHelper.SetValue(normalFlyoutKeyName, null, "Normal");

            string normalFlyoutCommandKeyName = Path.Combine(normalFlyoutKeyName, "command");
            RegistryHelper.SetValue(normalFlyoutCommandKeyName, null, "cmd /c start \"\" /Normal \"%1\"");

            string belowNormalFlyoutKeyName = Path.Combine(PRIORITY_KEY_NAME, "shell", "004flyout");
            RegistryHelper.SetValue(belowNormalFlyoutKeyName, null, "Below Normal");

            string belowNormalFlyoutCommandKeyName = Path.Combine(belowNormalFlyoutKeyName, "command");
            RegistryHelper.SetValue(belowNormalFlyoutCommandKeyName, null, "cmd /c start \"\" /BelowNormal \"%1\"");

            string lowFlyoutKeyName = Path.Combine(PRIORITY_KEY_NAME, "shell", "005flyout");
            RegistryHelper.SetValue(lowFlyoutKeyName, null, "Low");

            string lowFlyoutCommandKeyName = Path.Combine(lowFlyoutKeyName, "command");
            RegistryHelper.SetValue(lowFlyoutCommandKeyName, null, "cmd /c start \"\" /Low \"%1\"");

            _runWithPriorityContextMenuConfigurationStore.CurrentSetting = IsEnabled();
        }

        public bool IsEnabled()
        {
            return RegistryHelper.KeyExists(PRIORITY_KEY_NAME);
        }
    }
}
