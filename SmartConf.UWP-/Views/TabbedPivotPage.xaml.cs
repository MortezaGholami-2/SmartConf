﻿using System;

using SmartConf.UWP.ViewModels;

using Windows.UI.Xaml.Controls;

namespace SmartConf.UWP.Views
{
    public sealed partial class TabbedPivotPage : Page
    {
        public TabbedPivotViewModel ViewModel { get; } = new TabbedPivotViewModel();

        public TabbedPivotPage()
        {
            InitializeComponent();
        }
    }
}
