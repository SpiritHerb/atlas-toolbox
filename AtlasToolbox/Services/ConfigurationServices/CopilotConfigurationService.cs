//using AtlasToolbox.Stores;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Windows.ApplicationModel;
//using Windows.Foundation;
//using Windows.Management.Deployment;


//namespace AtlasToolbox.Services.ConfigurationServices
//{
//    internal class CopilotConfigurationService : IConfigurationService
//    {
//        private const string COPILOT_PACKAGE = "Microsoft.Copilot";
//        private readonly ConfigurationStore _copilotConfigurationStore;

//        public CopilotConfigurationService([FromKeyedServices("Copilot")] ConfigurationStore copilotConfigurationStore) 
//        {
//            _copilotConfigurationStore = copilotConfigurationStore;
//        }

//        public void Disable()
//        {
//            PackageManager packageManager = new PackageManager();
//            Package copilotPackage = packageManager.FindPackageForUser(userSecurityId: "", COPILOT_PACKAGE);
//            throw new NotImplementedException();
//        }

//        public void Enable()
//        {
//            throw new NotImplementedException();
//        }

//        public bool IsEnabled()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
