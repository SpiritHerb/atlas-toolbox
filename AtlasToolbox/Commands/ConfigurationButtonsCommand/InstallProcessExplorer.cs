using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Utils;
using MVVMEssentials.Commands;

namespace AtlasToolbox.Commands.ConfigurationButtonsCommand
{
    public class InstallProcessExplorer : AsyncCommandBase
    {
        protected override async Task ExecuteAsync(object parameter)
        {
            await Task.Run(() => { CommandPromptHelper.RunCommand(@$"{Environment.GetEnvironmentVariable("windir")}AtlasDesktop\6. Advanced Configuration\Process Explorer\Install Process Explorer.cmd", false); });
        }
    }
}
