using Microsoft.UI.Xaml.Controls;
using AtlasToolbox.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AtlasToolbox.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class GeneralConfig : Page
{
    private readonly GeneralConfigViewModel _viewModel;
    public GeneralConfig()
    {
        this.InitializeComponent();
        _viewModel = App._host.Services.GetRequiredService<GeneralConfigViewModel>();
        this.DataContext = _viewModel;
    }
}
