using System.Windows.Controls;

using SmartConf.WPF.ViewModels;

namespace SmartConf.WPF.Views;

public partial class SettingsPage : Page
{
    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
