using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Utils
{
    internal interface IDialogService
    {
        void SetXamlRoot(XamlRoot xamlRoot);
        void ShowDialog(string type);
    }
}
