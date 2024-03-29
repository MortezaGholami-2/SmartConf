﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using SmartConf.WPF.Contracts.ViewModels;
using SmartConf.WPF.Core.Contracts.Services;
using SmartConf.WPF.Core.Models;

namespace SmartConf.WPF.ViewModels;

public class ContentGridDetailViewModel : ObservableObject, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;
    private SampleOrder _item;

    public SampleOrder Item
    {
        get { return _item; }
        set { SetProperty(ref _item, value); }
    }

    public ContentGridDetailViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is long orderID)
        {
            var data = await _sampleDataService.GetContentGridDataAsync();
            Item = data.First(i => i.OrderID == orderID);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
