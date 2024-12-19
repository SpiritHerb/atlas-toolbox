using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using AtlasToolbox.Views;
using CommunityToolkit.WinUI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.Core;

namespace AtlasToolbox.Utils
{
    //public class DialogService : IDialogService
    //{
    //    private XamlRoot _xamlRoot;

    //    public void SetXamlRoot(XamlRoot xamlRoot)
    //    {
    //        _xamlRoot = xamlRoot;
    //    }

    //    public async void ShowMessageDialog(string title, string message)
    //    {
            
           
    //    }

    //}

    public static class ComputerStateHelper
    {
        public static void LogOffComputer()
        {
            CommandPromptHelper.RunCustomFile("C:\\Windows\\AtlasModules\\Scripts\\logoffPrompt.bat");
        }
    }
}