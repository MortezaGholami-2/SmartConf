using System.Windows.Controls;

using MahApps.Metro.Controls;

using SmartConf.WPF.Contracts.Views;
using SmartConf.WPF.ViewModels;

namespace SmartConf.WPF.Views;

public partial class ShellDialogWindow : MetroWindow, IShellDialogWindow
{
    public ShellDialogWindow(ShellDialogViewModel viewModel)
    {
        InitializeComponent();
        viewModel.SetResult = OnSetResult;
        DataContext = viewModel;
    }

    public Frame GetDialogFrame()
        => dialogFrame;

    private void OnSetResult(bool? result)
    {
        DialogResult = result;
        Close();
    }
}
