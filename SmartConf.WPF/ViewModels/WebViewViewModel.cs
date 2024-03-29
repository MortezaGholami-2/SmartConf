﻿using System.Windows;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MahApps.Metro.Controls;

using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

using SmartConf.WPF.Contracts.Services;
using SmartConf.WPF.Contracts.ViewModels;

namespace SmartConf.WPF.ViewModels;

public class WebViewViewModel : ObservableObject, INavigationAware
{
    // TODO: Set the URI of the page to show by default
    private const string DefaultUrl = "https://docs.microsoft.com/windows/apps/";
    private readonly IRightPaneService _rightPaneService;

    private readonly ISystemService _systemService;

    private string _source;
    private bool _isLoading = true;
    private bool _isShowingFailedMessage;
    private Visibility _isLoadingVisibility = Visibility.Visible;
    private Visibility _failedMesageVisibility = Visibility.Collapsed;
    private ICommand _refreshCommand;
    private RelayCommand _browserBackCommand;
    private RelayCommand _browserForwardCommand;
    private ICommand _openInBrowserCommand;
    private WebView2 _webView;

    public string Source
    {
        get { return _source; }
        set { SetProperty(ref _source, value); }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            SetProperty(ref _isLoading, value);
            IsLoadingVisibility = value ? Visibility.Visible : Visibility.Collapsed;
        }
    }

    public bool IsShowingFailedMessage
    {
        get => _isShowingFailedMessage;
        set
        {
            SetProperty(ref _isShowingFailedMessage, value);
            FailedMesageVisibility = value ? Visibility.Visible : Visibility.Collapsed;
        }
    }

    public Visibility IsLoadingVisibility
    {
        get { return _isLoadingVisibility; }
        set { SetProperty(ref _isLoadingVisibility, value); }
    }

    public Visibility FailedMesageVisibility
    {
        get { return _failedMesageVisibility; }
        set { SetProperty(ref _failedMesageVisibility, value); }
    }

    public ICommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new RelayCommand(OnRefresh));

    public RelayCommand BrowserBackCommand => _browserBackCommand ?? (_browserBackCommand = new RelayCommand(() => _webView?.GoBack(), () => _webView?.CanGoBack ?? false));

    public RelayCommand BrowserForwardCommand => _browserForwardCommand ?? (_browserForwardCommand = new RelayCommand(() => _webView?.GoForward(), () => _webView?.CanGoForward ?? false));

    public ICommand OpenInBrowserCommand => _openInBrowserCommand ?? (_openInBrowserCommand = new RelayCommand(OnOpenInBrowser));

    public WebViewViewModel(ISystemService systemService, IRightPaneService rightPaneService)
    {
        _systemService = systemService;
        Source = DefaultUrl;
        _rightPaneService = rightPaneService;
    }

    public void Initialize(WebView2 webView)
    {
        _webView = webView;
    }

    public void OnNavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        IsLoading = false;
        if (e != null && !e.IsSuccess)
        {
            // Use `e.WebErrorStatus` to vary the displayed message based on the error reason
            IsShowingFailedMessage = true;
        }

        BrowserBackCommand.NotifyCanExecuteChanged();
        BrowserForwardCommand.NotifyCanExecuteChanged();
    }

    private void OnRefresh()
    {
        IsShowingFailedMessage = false;
        IsLoading = true;
        _webView?.Reload();
    }

    private void OnOpenInBrowser()
        => _systemService.OpenInWebBrowser(Source);

    public void OnNavigatedTo(object parameter)
    {
        _rightPaneService.PaneOpened += OnRightPaneOpened;
        _rightPaneService.PaneClosed += OnRightPaneClosed;
    }

    public void OnNavigatedFrom()
    {
        _rightPaneService.PaneOpened -= OnRightPaneOpened;
        _rightPaneService.PaneClosed -= OnRightPaneClosed;
    }

    private void OnRightPaneOpened(object sender, System.EventArgs e)
    {
        // WebView control is always rendered on top
        // We need to adapt the WebView to be able to show the right pane
        if (sender is SplitView splitView)
        {
            _webView.Margin = new Thickness(0, 0, splitView.OpenPaneLength, 0);
        }
    }

    private void OnRightPaneClosed(object sender, System.EventArgs e)
    => _webView.Margin = new Thickness(0);
}
