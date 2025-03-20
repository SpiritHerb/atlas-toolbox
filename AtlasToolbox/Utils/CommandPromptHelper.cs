using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace AtlasToolbox.Utils
{
    public class CommandPromptHelper
    {
        /// <summary>
        /// Runs any given command in parameters
        /// </summary>
        /// <param name="command">command</param>
        /// <param name="noWindow">True by default</param>
        public static void RunCommand(string command, bool noWindow= true)
        {
            Process commandPrompt = new Process();
            commandPrompt.StartInfo.FileName = "cmd.exe";
            commandPrompt.StartInfo.Arguments = $"/c {command}";
            commandPrompt.StartInfo.CreateNoWindow = noWindow;
            commandPrompt.StartInfo.UseShellExecute = false;

            commandPrompt.Start();
            commandPrompt.WaitForExit();
        }
        /// <summary>
        /// Restarts explorer.exe
        /// if there's a better way to do this, please make a PR
        /// </summary>
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
            startExplorer.StartInfo.Arguments = $"/c explorer.exe";
            startExplorer.StartInfo.CreateNoWindow = true;
            startExplorer.StartInfo.UseShellExecute = false;

            startExplorer.Start();
            startExplorer.WaitForExit();
        }
    }
}
