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
        ////public static void RestartCommandWindow()
        ////{
        ////    string msgtext = "Would you like to restart to apply the changes?";
        ////    string txt = "Restart to apply changes";
        ////    MessageBoxButton button = MessageBoxButton.YesNo;
        ////    MessageBoxResult result = MessageBox.Show(msgtext, txt, button);

        ////    switch (result)
        ////    {
        ////        case MessageBoxResult.Yes:
        ////            CommandPromptHelper.RunCommand("logoff");
        ////            break;
        ////        case MessageBoxResult.No:

        ////            break;
        ////    }
        //    //Thread restartWindowThread = new Thread(new ThreadStart(() =>
        //    //{
        //    //    RestartComputerView restartComputerView = new RestartComputerView();
                
        //    //    restartComputerView.ShowDialog();

        //    //    System.Windows.Threading.Dispatcher.Run();
        //    //}));
        //    //restartWindowThread.SetApartmentState(ApartmentState.STA);
        //    //restartWindowThread.IsBackground = false;
        //    //restartWindowThread.Start();
        //}

        //public static void LogOffCommandWindow()
        //{

        //    string msgtext = "Would you like to logout to apply the changes?";
        //    string txt = "Logout to apply changes";
        //    MessageBoxButton button = MessageBoxButton.YesNo;
        //    MessageBoxResult result = MessageBox.Show(msgtext, txt, button);

        //    switch (result)
        //    {
        //        case MessageBoxResult.Yes:
        //            CommandPromptHelper.RunCommand("logoff");
        //            break;
        //        case MessageBoxResult.No:

        //            break;
        //    }
        //    //Thread logOffWindowThread = new Thread(new ThreadStart(() =>
        //    //{
        //    //    LogOffComputerView logOffComputerView = new LogOffComputerView();

        //    //    logOffComputerView.ShowDialog();

        //    //    System.Windows.Threading.Dispatcher.Run();
        //    //}));

        //    //logOffWindowThread.SetApartmentState(ApartmentState.STA);
        //    //logOffWindowThread.IsBackground = false;
        //    //logOffWindowThread.Start();
        //}
    }
}
