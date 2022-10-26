using System.Windows.Controls;

using SmartConf.WPF.ViewModels;

namespace SmartConf.WPF.Views;

public partial class ContentGridDetailPage : Page
{
    public ContentGridDetailPage(ContentGridDetailViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
