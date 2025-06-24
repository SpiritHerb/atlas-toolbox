using System;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Animation;
using System.Runtime.InteropServices;
using AtlasToolbox.Utils;
using CommunityToolkit.WinUI;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using AtlasToolbox.Views;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web.Provider;
using Microsoft.UI;
using WinRT.Interop;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using ICSharpCode.Decompiler.CSharp.Syntax;
using AtlasToolbox.ViewModels;
using System.Runtime.CompilerServices;
using Windows.Graphics;
using Microsoft.UI.Windowing;

namespace AtlasToolbox
{
    public sealed partial class MainWindow : Window
    {
        public List<IConfigurationItem> RootList { get; set; }
        public MainWindow()
        {
            this.InitializeComponent();

            //Window parameters
            AppWindow.Resize(new Windows.Graphics.SizeInt32(1250, 850));
            AppWindow.TitleBar.PreferredTheme = TitleBarTheme.UseDefaultAppMode;

            OverlappedPresenter presenter = OverlappedPresenter.Create();
            presenter.PreferredMinimumWidth = 516;
            presenter.PreferredMinimumHeight = 491;
            presenter.IsMaximizable = true;
            AppWindow.SetPresenter(presenter);

            CenterWindowOnScreen();
            ExtendsContentIntoTitleBar = true;

            LoadText();

            // Setup root list
            RootList = new List<IConfigurationItem>();
            foreach (IConfigurationItem item in App._host.Services.GetServices<LinksViewModel>())
            {
                /*if (!item.Type.ToString().Contains("SubMenu"))*/ RootList.Add(item);
            }
            foreach (IConfigurationItem item in App._host.Services.GetServices<ConfigurationItemViewModel>())
            {
                /*if (!item.Type.ToString().Contains("SubMenu"))*/ RootList.Add(item);
            }
            foreach (IConfigurationItem item in App._host.Services.GetServices<MultiOptionConfigurationItemViewModel>())
            {
                /*if (!item.Type.ToString().Contains("SubMenu"))*/ RootList.Add(item);
            }
            foreach (IConfigurationItem item in App._host.Services.GetServices<ConfigurationSubMenuViewModel>())
            {
                /*if (!item.Type.ToString().Contains("SubMenu"))*/ RootList.Add(item);
            }
            foreach (IConfigurationItem item in App._host.Services.GetServices<ConfigurationButtonViewModel>())
            {
                /*if (!item.Type.ToString().Contains("SubMenu"))*/ RootList.Add(item);
            }
            App.RootList = this.RootList;
            NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems.OfType<NavigationViewItem>().First();
            ContentFrame.Navigate(
                       typeof(Views.HomePage),
                       null,
                       new Microsoft.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo()
                       );
            SetTitleBar(AppTitleBar);
            CheckUpdates();
            if (RegistryHelper.IsMatch("HKLM\\SOFTWARE\\AtlasOS\\Toolbox", "OnStartup", 1)) this.Closed += AppBehaviorHelper.HideApp;
            else this.Closed += AppBehaviorHelper.CloseApp;            
        }

        private async void CheckUpdates()
        {
            bool update = await Task.Run(() => ToolboxUpdateHelper.CheckUpdates());
            if (update)
            {
                UpdateTitleBar.IsOpen = true;
            }
        }
        public void LoadText()
        {
            UpdateTitleBar.Title = App.GetValueFromItemList("NewUpdateMessage");
            LearnMoreBtn.Content = App.GetValueFromItemList("LearnMore");
            Home.Content = App.GetValueFromItemList("Home_HeaderText");
            Software.Content = App.GetValueFromItemList("Software");
            GeneralConfig.Content = App.GetValueFromItemList("GeneralConfig");
            Interface.Content = App.GetValueFromItemList("Interface");
            Windows.Content = App.GetValueFromItemList("Windows");
            Advanced.Content = App.GetValueFromItemList("Advanced");
            Security.Content = App.GetValueFromItemList("Security");
            Troubleshooting.Content = App.GetValueFromItemList("Troubleshooting");
            Setting.Content = App.GetValueFromItemList("Settings");
        }

        /// <summary>
        /// Gets the window Xaml root for ContentDialogs
        /// </summary>
        /// <returns></returns>
        public XamlRoot GetXamlRoot()
        {
            return this.Content.XamlRoot;
        }

        /// <summary>
        /// navigates to the correct page when a navigation item is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void NavigationViewControl_ItemInvoked(NavigationView sender,
                      NavigationViewItemInvokedEventArgs args)
        {
            //var NavView = sender as NavigationView;
            //if (NavView.SelectedItem == args.InvokedItemContainer) { return; };

            if (App.CurrentCategory == args.InvokedItemContainer.Tag.ToString() || (App.CurrentCategory == "SettingsItem" && args.IsSettingsInvoked == true)) { return; }
            ;

            App.CurrentCategory = args.InvokedItemContainer.Tag.ToString();
            if (args.IsSettingsInvoked == true)
            {
                App.CurrentCategory = "SettingsItem";
                ContentFrame.Navigate(typeof(Views.SettingsPage), null, new DrillInNavigationTransitionInfo());
                return;
            }

            Navigate(args.InvokedItemContainer.Tag.ToString());
            App.XamlRoot = this.Content.XamlRoot;
        }

        private void Navigate(string tag)
        {
            switch (tag)
            {
                case "SettingsPage":
                    App.CurrentCategory = "SettingsItem";
                    ContentFrame.Navigate(typeof(Views.SettingsPage), null, new DrillInNavigationTransitionInfo());
                    break;
                case "AtlasToolbox.Views.SoftwarePage":
                    ContentFrame.Navigate(
                           new SoftwarePage().GetType(),
                           null,
                           new DrillInNavigationTransitionInfo()
                           );
                    break;
                case "AtlasToolbox.Views.HomePage":
                    Type newPage = Type.GetType(tag);
                    ContentFrame.Navigate(
                           newPage,
                           null,
                           new DrillInNavigationTransitionInfo());
                    break;
                default:
                    ContentFrame.Navigate(
                           new ConfigPage().GetType(),
                           null,
                           new DrillInNavigationTransitionInfo());
                    break;
            }
        }

        public void GoBack()
        {
            if (ContentFrame.CanGoBack) ContentFrame.GoBack();
        }
        public void NavigationViewControl_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack) ContentFrame.GoBack();
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            NavigateTo();
        }

        private void NavigateTo()
        {
            NavigationViewControl.IsBackEnabled = ContentFrame.CanGoBack;
            NavigationViewControl.Header = null;

            if (ContentFrame.SourcePageType != typeof(Views.SubSection))
            {
                if (App.CurrentCategory != null && App.CurrentCategory != "SettingsItem")
                {
                    try
                    {
                        NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems
                            .OfType<NavigationViewItem>()
                            .First(n => n.Tag.Equals(App.CurrentCategory));
                    }
                    catch (InvalidOperationException)
                    {
                        App.logger.Error($"No matching NavigationViewItem found for category: {App.CurrentCategory}");
                    }
                }
                else
                {
                    NavigationViewControl.SelectedItem = (NavigationViewItem)NavigationViewControl.SettingsItem;
                }
            }

            if (ContentFrame.SourcePageType == typeof(Views.SettingsPage))
            {
                NavigationViewControl.SelectedItem = (NavigationViewItem)NavigationViewControl.SettingsItem;
            }
        }


        /// <summary>
        /// Creates a ContentDialog with the required type
        /// </summary>
        /// <param name="type">type of content dialog</param>
        /// <exception cref="Exception"></exception>
        public async void ContentDialogContoller(string type)
        {
            string title = "", desc = "", primBtnTxt = "";
            ICommand command = null;

            switch (type)
            {
                case "newUpdate":
                    title = App.GetValueFromItemList("NewUpdate");
                    desc = App.GetValueFromItemList("NewUpdateDesc");
                    primBtnTxt = App.GetValueFromItemList("Yes");
                    command = new RelayCommand(ToolboxUpdateHelper.InstallUpdate);
                    break;
                case "restartApp":
                    title = App.GetValueFromItemList("RestartApp");
                    desc = App.GetValueFromItemList("RestartAppDesc");
                    primBtnTxt = App.GetValueFromItemList("RestartAppBtn");
                    command = new RelayCommand(ComputerStateHelper.RestartApp);
                    break;
                case "restart":
                    title = App.GetValueFromItemList("RestartPC");
                    desc = App.GetValueFromItemList("RestartPCDesc");
                    primBtnTxt = App.GetValueFromItemList("RestartAppBtn");
                    command = new RelayCommand(ComputerStateHelper.RestartComputer);
                    break;
                case "logoff":
                    title = App.GetValueFromItemList("RelogApply");
                    desc = App.GetValueFromItemList("RelogApplyDesc");
                    primBtnTxt = App.GetValueFromItemList("RelogBtn");
                    command = new RelayCommand(ComputerStateHelper.LogOffComputer);
                    break;
                default:
                    throw new Exception("ContentDialog type was not set or does not match any possible type");
            }
            await DispatcherQueue.EnqueueAsync(() =>
            {
                ContentDialog dialog = new ContentDialog();

                // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
                dialog.XamlRoot = App.XamlRoot;
                dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
                dialog.Title = title;
                dialog.Content = desc;
                dialog.PrimaryButtonText = primBtnTxt;
                dialog.CloseButtonText = App.GetValueFromItemList("Later");
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.PrimaryButtonCommand = command;

                try
                {
                    var result = dialog.ShowAsync();
                }
                catch
                { App.logger.Error("Program tried to open more than one ContentDialog"); }
            });
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

        int timesClicked;
        private void AtlasButton_Click(object sender, RoutedEventArgs e)
        {
            if (timesClicked == 10)
            {
                App.f_window = new FWindow();
                App.f_window.Activate();
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
            }
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var configItem = RootList.Where(item => item.Name == args.SelectedItem.ToString()).FirstOrDefault();
            string type = configItem.Type.ToString();

            if (configItem is not null)
            {
                NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems
                                .OfType<NavigationViewItem>()
                                .First(n => n.Tag.Equals(configItem.Type.ToString()));
                App.CurrentCategory = configItem.Type.ToString();
                Navigate(configItem.Type.ToString());
            }
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Since selecting an item will also change the text,
            // only listen to changes caused by user entering text.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suitableItems = new List<string>();
                var splitText = sender.Text.ToLower().Split(" ");
                foreach (var viewModel in RootList)
                {
                    var found = splitText.All((key) =>
                    {
                        return viewModel.Name.ToLower().Contains(key);
                    });
                    if (found)
                    {
                        suitableItems.Add(viewModel.Name);
                    }
                }
                if (suitableItems.Count == 0)
                {
                    suitableItems.Add("No results found");
                }
                sender.ItemsSource = suitableItems;
            }
        }
    }
}
