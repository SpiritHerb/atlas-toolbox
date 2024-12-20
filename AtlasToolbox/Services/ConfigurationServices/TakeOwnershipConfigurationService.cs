using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
namespace AtlasToolbox.Services.ConfigurationServices
{
    public class TakeOwnershipConfigurationService : IConfigurationService
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\TakeOwnership";
        private const string STATE_VALUE_NAME = "state";

        private const string TAKE_OWNERSHIP_KEY_NAME = @"HKCR\*\shell\TakeOwnership";
        private const string RUNAS_KEY_NAME = @"HKCR\*\shell\runas";
        private const string TAKE_OWNERSHIP_DIRECTORY_KEY_NAME = @"HKCR\Directory\shell\TakeOwnership";
        private const string RUNAS_DRIVE_KEY_NAME = @"HKCR\Drive\shell\runas";

        private readonly ConfigurationStore _takeOwnership;

        public TakeOwnershipConfigurationService(
                    [FromKeyedServices("TakeOwnership")] ConfigurationStore takeOwnership)
        {
            _takeOwnership = takeOwnership;
        }
        public void Disable()
        {
            RegistryHelper.DeleteKey(RUNAS_DRIVE_KEY_NAME);
            RegistryHelper.DeleteKey(TAKE_OWNERSHIP_DIRECTORY_KEY_NAME);
            RegistryHelper.DeleteKey(TAKE_OWNERSHIP_KEY_NAME);
            RegistryHelper.DeleteKey(RUNAS_KEY_NAME);

        }

        public void Enable()
        {
            

        }

        public bool IsEnabled()
        {
            throw new NotImplementedException();
        }
    }
}

