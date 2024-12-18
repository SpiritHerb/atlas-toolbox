using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using AtlasToolbox.Views;

namespace AtlasToolbox.Utils
{
    class ComputerStateHelper
    {
        public static void RestartCommandWindow()
        {
            CommandPromptHelper.RunCustomFile(@"C:\Windows\AtlasModules\Scripts\logoffPrompt.cmd");
        }

        public static void LogOffCommandWindow()
        {
            CommandPromptHelper.RunCustomFile(@"C:\Windows\AtlasModules\Scripts\logoffPrompt.cmd");
        }
    }
}
