using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Utils;
using MVVMEssentials.Commands;

namespace AtlasToolbox.Commands
{
    public class RestartExplorerCommand : AsyncCommandBase
    {
        protected override async Task ExecuteAsync(object parameter)
        {
            App.ContentDialogCaller("validate");
            await Task.Run(() => { CommandPromptHelper.RunCommand(@$"{Environment.GetEnvironmentVariable("windir")}\AtlasModules\Toolbox\Scripts\setServicesToDefaults.cmd", false); });
        }
    }
}
