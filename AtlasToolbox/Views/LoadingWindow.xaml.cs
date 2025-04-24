using Microsoft.UI.Xaml;
using System;
using System.Runtime.InteropServices;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AtlasToolbox
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            this.InitializeComponent();
            LoadText();
            WindowManager.Get(this).Width = 250;
            WindowManager.Get(this).MinWidth = 250;

            WindowManager.Get(this).Height = 250;
            WindowManager.Get(this).MinHeight = 250;

            WindowManager.Get(this).IsResizable = false;
            WindowManager.Get(this).IsMaximizable = false;
            WindowManager.Get(this).IsTitleBarVisible = false;
            CenterWindowOnScreen();
            ExtendsContentIntoTitleBar = true;
        }

        private void LoadText()
        {
            StartingServices.Text = App.GetValueFromItemList("StartingServices");
        }

        private void CenterWindowOnScreen()
        {
            var screenWidth = GetSystemMetrics(SM_CXSCREEN);
            var screenHeight = GetSystemMetrics(SM_CYSCREEN);

            double centerX = (screenWidth - this.Bounds.Width) / 2;
            double centerY = (screenHeight - this.Bounds.Height) / 2;

            this.MoveAndResize(centerX, centerY, this.Bounds.Width, this.Bounds.Height);
        }

        private void MoveAndResize(double x, double y, double width, double height)
        {
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            SetWindowPos(hwnd, IntPtr.Zero, (int)x, (int)y, (int)width, (int)height, SWP_NOZORDER | SWP_NOACTIVATE);
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOACTIVATE = 0x0010;
        }
    }
