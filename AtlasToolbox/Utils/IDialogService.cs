using Microsoft.UI.Xaml;

public interface IDialogService
{
    void SetXamlRoot(XamlRoot xamlRoot);
    void ShowMessageDialog(string title, string message);
}
