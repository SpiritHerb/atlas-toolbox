using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Utils
{
    public class CommandPromptHelper
    {
        public static void RunCommand(string command)
        {
            Process commandPrompt = new Process();
            commandPrompt.StartInfo.FileName = "cmd.exe";
            commandPrompt.StartInfo.Arguments = $"/c {command}";
            commandPrompt.StartInfo.CreateNoWindow = true;
            commandPrompt.StartInfo.UseShellExecute = false;

            commandPrompt.Start();
            commandPrompt.WaitForExit();
        }

        public static void RestartExplorer()
        {
            Process stopExplorer = new Process();
            stopExplorer.StartInfo.FileName = "cmd.exe";
            stopExplorer.StartInfo.Arguments = $"/c taskkill /f /im explorer.exe";
            stopExplorer.StartInfo.CreateNoWindow = true;
            stopExplorer.StartInfo.UseShellExecute = false;

            stopExplorer.Start();
            stopExplorer.WaitForExit();

            Process startExplorer = new Process();
            startExplorer.StartInfo.FileName = "cmd.exe";
            startExplorer.StartInfo.Arguments = $"/c taskkill /f /im explorer.exe";
            startExplorer.StartInfo.CreateNoWindow = true;
            startExplorer.StartInfo.UseShellExecute = false;

            startExplorer.Start();
            startExplorer.WaitForExit();
        }
    }
}
