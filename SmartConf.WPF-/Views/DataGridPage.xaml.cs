using System.Windows.Controls;

using SmartConf.WPF.ViewModels;

namespace SmartConf.WPF.Views;

public partial class DataGridPage : Page
{
    public DataGridPage(DataGridViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
