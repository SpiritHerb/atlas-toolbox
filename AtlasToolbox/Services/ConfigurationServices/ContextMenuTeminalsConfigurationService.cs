using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AtlasToolbox.Stores;
using AtlasToolbox.Utils;
using ICSharpCode.Decompiler.CSharp.Syntax;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public class ContextMenuTeminalsConfigurationService : IMultiOptionConfigurationServices
    {

        private const string ATLAS_STORE_KEY_NAME = @"HKLM\SOFTWARE\AtlasOS\ContextMenuTerminals";
        private const string STATE_VALUE_NAME = "state";

        private readonly ConfigurationStore _contextMenuTeminalsConfigurationService;

        private const string CONTEXT_MENU_REG_FILE_PATH = "C:\\Windows\\AtlasModules\\Scripts\\ConfigurationServices\\ContextMenuTerminals\\ContextMenuTerminals_";
        public void ChangeStatus(int status)
        {
            RegistryHelper.MergeRegFile(CONTEXT_MENU_REG_FILE_PATH + status.ToString());

            if (status == 0)
            {
                RegistryHelper.DeleteKey(ATLAS_STORE_KEY_NAME);
            }
            else
            {
                RegistryHelper.SetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME, status);
            }
        }

        public byte Status()
        {
            return (byte)RegistryHelper.GetValue(ATLAS_STORE_KEY_NAME, STATE_VALUE_NAME);
        }
    }
}
