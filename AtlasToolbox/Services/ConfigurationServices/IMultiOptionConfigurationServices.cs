using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Services.ConfigurationServices
{
    public interface IMultiOptionConfigurationServices
    {
        void ChangeStatus(int status);
        string Status();
    }
}
