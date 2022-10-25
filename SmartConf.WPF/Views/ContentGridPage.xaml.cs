using System.Windows.Controls;

using SmartConf.WPF.ViewModels;

namespace SmartConf.WPF.Views;

public partial class ContentGridPage : Page
{
    public ContentGridPage(ContentGridViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
