//using System;
//using Microsoft.UI.Xaml;
//using Microsoft.UI.Xaml.Controls;
//using AtlasOSToolbox;

//namespace AtlasToolbox.Utils
//{
//    //public class DialogService : IDialogService
//    //{
//    //    private XamlRoot _xamlRoot;

//    //    public void SetXamlRoot(XamlRoot xamlRoot)
//    //    {
//    //        _xamlRoot = xamlRoot;
//    //    }

//    //    public async void ShowMessageDialog(string title, string message)
//    //    {
            
           
//    //    }

//    //}
//    public static class ComputerStateHelper
//    {
//        public static UIElement GetXamlRoot()
//        {
//            if (App.m_window.Content is UIElement rootFrame)
//            {
//                return rootFrame;
//            }
//            return null;
//        }
//        public async static void LogOffComputer()
//        {
//            //CommandPromptHelper.RunCustomFile("C:\\Windows\\AtlasModules\\Scripts\\logoffPrompt.bat");
//            //UIElement rootElement = GetXamlRoot();


//            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
//            App.dialog.XamlRoot = App.XamlRoot;
//            App.dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
//            App.dialog.Title = "Do you really wish to delete this profile?";
//            App.dialog.PrimaryButtonText = "Yes";
//            App.dialog.CloseButtonText = "Cancel";
//            App.dialog.DefaultButton = ContentDialogButton.Primary;

//            var result = await App.dialog.ShowAsync();
//        }
//    }
//}