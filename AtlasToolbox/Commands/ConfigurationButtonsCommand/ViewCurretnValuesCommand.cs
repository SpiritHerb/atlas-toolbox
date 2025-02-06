using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Utils;
using MVVMEssentials.Commands;

namespace AtlasToolbox.Commands.ConfigurationButtonsCommand
{
    public class ViewCurretnValuesCommand : AsyncCommandBase
    {
        protected override async Task ExecuteAsync(object parameter)
        {
            await Task.Run(() => {
                CommandPromptHelper.RunCustomFile(@$"{Environment.GetEnvironmentVariable("windir")}\AtlasModules\Toolbox\Scripts\viewBootValues.cmd");
            });
        }
    }
}
