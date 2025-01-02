using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using WinUIEx;

namespace AtlasToolbox.Utils
{
    public static class AppBehaviorHelper
    {
        public static void HideApp(object sender, WindowEventArgs e)
        {
            e.Handled = true;
            App.m_window.Hide();
        }
        public static void CloseApp(object sender, WindowEventArgs e)
        {
            App.Current.Exit();
        }
    }
}
