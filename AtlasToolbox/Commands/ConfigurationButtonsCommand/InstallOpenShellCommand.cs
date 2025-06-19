using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasToolbox.Utils;
using MVVMEssentials.Commands;

namespace AtlasToolbox.Commands.ConfigurationButtonsCommand
{
    public class InstallOpenShellCommand : AsyncCommandBase
    {
        protected override async Task ExecuteAsync(object parameter)
        {
            await Task.Run(() => { ProcessHelper.StartShellExecute(@$"{Environment.GetEnvironmentVariable("windir")}\AtlasDesktop\4. Interface Tweaks\Start Menu\Install Open-Shell.cmd"); });
        }
    }
}
