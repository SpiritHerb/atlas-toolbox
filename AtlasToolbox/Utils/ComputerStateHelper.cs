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
    public class DialogService : IDialogService
    {
        private XamlRoot _xamlRoot;

        public void SetXamlRoot(XamlRoot xamlRoot)
        {
            _xamlRoot = xamlRoot;
        }

        public async void ShowMessageDialog(string title, string message)
        {
            if (_xamlRoot == null)
            {
                throw new InvalidOperationException("XamlRoot is not set.");
            }

            //ContentDialog dialog = new ContentDialog();
            //dialog.XamlRoot = _xamlRoot;
            //dialog.Title = title;
            //dialog.Content = message;
            //dialog.PrimaryButtonText = "OK";
            //await dialog.ShowAsync(); 

            //var dispatcherQueue = DispatcherQueue.GetForCurrentThread();
            //if (dispatcherQueue == null) { throw new InvalidOperationException("Dispatcherquue is not available."); }
            //dispatcherQueue.TryEnqueue(async () => {
            //    try
            //    { 
            //        ContentDialog dialog = new ContentDialog 
            //        { 
            //            XamlRoot = _xamlRoot,
            //            Title = title,
            //            Content = message, 
            //            PrimaryButtonText = "OK" 
            //        };
            //        await dialog.ShowAsync(); 
            //    }
            //    catch (System.Runtime.InteropServices.COMException ex)
            //    {   System.Diagnostics.Debug.WriteLine($"COMException Error Code: {ex.ErrorCode}"); 
            //        System.Diagnostics.Debug.WriteLine($"COMException Message: {ex.Message}"); 
            //        throw; 
            //    } 
            //    catch (Exception ex) 
            //    { // Log other exceptions
            //      System.Diagnostics.Debug.WriteLine($"Error showing dialog: {ex.Message}"); throw; } 
            //});if (_xamlRoot == null) { throw new InvalidOperationException("XamlRoot is not set."); }
            //var dispatcherQueue = DispatcherQueue.GetForCurrentThread();
            //if (dispatcherQueue == null) { throw new InvalidOperationException("Dispatcherquue is not available."); }
            //dispatcherQueue.TryEnqueue(async () => {
            //    try
            //    { 
            //        ContentDialog dialog = new ContentDialog 
            //        { 
            //            XamlRoot = _xamlRoot,
            //            Title = title,
            //            Content = message, 
            //            PrimaryButtonText = "OK" 
            //        };
            //        await dialog.ShowAsync(); 
            //    }
            //    catch (System.Runtime.InteropServices.COMException ex)
            //    {   System.Diagnostics.Debug.WriteLine($"COMException Error Code: {ex.ErrorCode}"); 
            //        System.Diagnostics.Debug.WriteLine($"COMException Message: {ex.Message}"); 
            //        throw; 
            //    } 
            //    catch (Exception ex) 
            //    { // Log other exceptions
            //      System.Diagnostics.Debug.WriteLine($"Error showing dialog: {ex.Message}"); throw; } 
            //});
        }

    }
}