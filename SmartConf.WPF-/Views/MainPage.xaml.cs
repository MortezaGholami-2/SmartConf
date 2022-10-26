using System.Windows.Controls;

using SmartConf.WPF.ViewModels;

namespace SmartConf.WPF.Views;

public partial class MainPage : Page
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
