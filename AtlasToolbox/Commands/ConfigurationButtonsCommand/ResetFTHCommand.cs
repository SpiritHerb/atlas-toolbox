using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Utils;
using MVVMEssentials.Commands;

namespace AtlasToolbox.Commands.ConfigurationButtonsCommand
{
    public class ResetFTHCommand : AsyncCommandBase
    {
        protected override async Task ExecuteAsync(object parameter)
        {
            await Task.Run(() => { CommandPromptHelper.RunCommand(@$"{Environment.GetEnvironmentVariable("windir")}\System32\rundll32.exe", false); });
        }
    }
}
