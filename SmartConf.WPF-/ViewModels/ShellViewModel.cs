using System.Windows;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using SmartConf.WPF.Contracts.Services;
using SmartConf.WPF.Core.Models;
using SmartConf.WPF.Properties;

namespace SmartConf.WPF.ViewModels;

// You can show pages in different ways (update main view, navigate, right pane, new windows or dialog)
// using the NavigationService, RightPaneService and WindowManagerService.
// Read more about MenuBar project type here:
// https://github.com/microsoft/TemplateStudio/blob/main/docs/WPF/projectTypes/menubar.md
public class ShellViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IRightPaneService _rightPaneService;

    private RelayCommand _goBackCommand;
    private ICommand _menuFileOpenConfigFileCommand;
    private ICommand _menuFileSettingsCommand;
    private ICommand _menuViewsListDetailsCommand;
    private ICommand _menuViewsContentGridCommand;
    private ICommand _menuViewsDataGridCommand;
    private ICommand _menuViewsMainCommand;
    private ICommand _menuFileExitCommand;
    private ICommand _loadedCommand;
    private ICommand _unloadedCommand;

    public RelayCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new RelayCommand(OnGoBack, CanGoBack));

    public ICommand MenuFileOpenConfigFileCommand => _menuFileOpenConfigFileCommand ?? (_menuFileOpenConfigFileCommand = new RelayCommand(OnMenuFileOpenConfigFile));

    public ICommand MenuFileSettingsCommand => _menuFileSettingsCommand ?? (_menuFileSettingsCommand = new RelayCommand(OnMenuFileSettings));

    public ICommand MenuFileExitCommand => _menuFileExitCommand ?? (_menuFileExitCommand = new RelayCommand(OnMenuFileExit));

    public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(OnLoaded));

    public ICommand UnloadedCommand => _unloadedCommand ?? (_unloadedCommand = new RelayCommand(OnUnloaded));

    public ShellViewModel(INavigationService navigationService, IRightPaneService rightPaneService)
    {
        _navigationService = navigationService;
        _rightPaneService = rightPaneService;
    }

    private void OnLoaded()
    {
        _navigationService.Navigated += OnNavigated;
    }

    private void OnUnloaded()
    {
        _rightPaneService.CleanUp();
        _navigationService.Navigated -= OnNavigated;
    }

    private bool CanGoBack()
        => _navigationService.CanGoBack;

    private void OnGoBack()
        => _navigationService.GoBack();

    private void OnNavigated(object sender, string viewModelName)
    {
        GoBackCommand.NotifyCanExecuteChanged();
    }

    private void OnMenuFileOpenConfigFile()
    {
        var fileContent = string.Empty;


        OpenFileDialog openFileDialog = new()
        {
            InitialDirectory = "c:\\",
            Filter = "DOSBox-X config files (*.conf)|*.conf|All files (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = true
        };
        bool? result = openFileDialog.ShowDialog();
        if (result == true)
        {
            string filePath = openFileDialog.FileName;

            ConfigFile configFile = new(filePath);
            List<ConfigurationItem> configurations = configFile.ReadFile();
            _navigationService.NavigateTo(typeof(MainViewModel).FullName, configurations, true);
            //    //LoadConfigurations(configurations);




        }
    }

    private void OnMenuFileExit()
        => Application.Current.Shutdown();

    public ICommand MenuViewsMainCommand => _menuViewsMainCommand ?? (_menuViewsMainCommand = new RelayCommand(OnMenuViewsMain));

    private void OnMenuViewsMain()
        => _navigationService.NavigateTo(typeof(MainViewModel).FullName, null, true);

    public ICommand MenuViewsDataGridCommand => _menuViewsDataGridCommand ?? (_menuViewsDataGridCommand = new RelayCommand(OnMenuViewsDataGrid));

    private void OnMenuViewsDataGrid()
        => _navigationService.NavigateTo(typeof(DataGridViewModel).FullName, null, true);

    public ICommand MenuViewsContentGridCommand => _menuViewsContentGridCommand ?? (_menuViewsContentGridCommand = new RelayCommand(OnMenuViewsContentGrid));

    private void OnMenuViewsContentGrid()
        => _navigationService.NavigateTo(typeof(ContentGridViewModel).FullName, null, true);

    public ICommand MenuViewsListDetailsCommand => _menuViewsListDetailsCommand ?? (_menuViewsListDetailsCommand = new RelayCommand(OnMenuViewsListDetails));

    private void OnMenuViewsListDetails()
        => _navigationService.NavigateTo(typeof(ListDetailsViewModel).FullName, null, true);

    private void OnMenuFileSettings()
        => _rightPaneService.OpenInRightPane(typeof(SettingsViewModel).FullName);
}
