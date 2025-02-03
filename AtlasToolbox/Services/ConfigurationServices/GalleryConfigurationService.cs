using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AtlasToolbox.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasToolbox.Services.ConfigurationServices
{
    internal class GalleryConfigurationService : IConfigurationService
    {
        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\Gallery";
        private const string STATE_VALUE_NAME = "state";

        private const string CLSID_KEY_NAME = @"HKCU\Software\Classes\CLSID\{e88865ea-0e1c-4e20-9aa6-edcd0212c87c}";

        private const string IS_PINNED_TO_NAME_SPACE_TREE = "System.IsPinnedToNameSpaceTree";

        private readonly ConfigurationStore _galleryConfigurationService;

        public GalleryConfigurationService(
            [FromKeyedServices("Gallery")] ConfigurationStore galleryConfigurationService)
        {
            _galleryConfigurationService = galleryConfigurationService;
        }
        public void Disable()
        {
            RegisteredWaitHandle
        }

        public void Enable()
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled()
        {
            throw new NotImplementedException();
        }
    }
}
