using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using AtlasOSToolbox;

namespace AtlasToolbox.Utils
{
    public static class ComputerStateHelper
    {
        public static void LogOffComputer()
        {
            CommandPromptHelper.RunCommand("logoff");
        }

        public static void RestartComputer()
        {
            CommandPromptHelper.RunCommand("shutdown /r");
        }
    }
}