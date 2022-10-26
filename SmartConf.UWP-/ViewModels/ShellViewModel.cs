using System;
using System.Collections.Generic;
using System.Windows.Input;
using DOSBox_X.Core.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

using SmartConf.UWP.Helpers;
using SmartConf.UWP.Services;
using SmartConf.UWP.Views;

using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace SmartConf.UWP.ViewModels
{
    public class ShellViewModel : ObservableObject
    {
        private readonly KeyboardAccelerator _altLeftKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu);
        private readonly KeyboardAccelerator _backKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.GoBack);
        private IList<KeyboardAccelerator> _keyboardAccelerators;

        private ICommand _loadedCommand;
        private ICommand _menuFileNewConfigFile;
        private ICommand _menuFileOpenConfigFile;
        private ICommand _menuViewsMainCommand;
        private ICommand _menuViewsListDetailsCommand;
        private ICommand _menuViewsContentGridCommand;
        private ICommand _menuViewsDataGridCommand;
        private ICommand _menuViewsTabbedPivotCommand;
        private ICommand _menuViewsImageGalleryCommand;
        private ICommand _menuFilesSettingsCommand;
        private ICommand _menuFileExitCommand;

        public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(OnLoaded));

        public ICommand MenuFileNewConfigFile => _menuFileNewConfigFile ?? (_menuFileNewConfigFile = new RelayCommand(OnMenuFileNewConfigFile));

        public ICommand MenuFileOpenConfigFile => _menuFileOpenConfigFile ?? (_menuFileOpenConfigFile = new RelayCommand(OnMenuFileOpenConfigFile));

        public ICommand MenuViewsMainCommand => _menuViewsMainCommand ?? (_menuViewsMainCommand = new RelayCommand(OnMenuViewsMain));

        public ICommand MenuViewsListDetailsCommand => _menuViewsListDetailsCommand ?? (_menuViewsListDetailsCommand = new RelayCommand(OnMenuViewsListDetails));

        public ICommand MenuViewsContentGridCommand => _menuViewsContentGridCommand ?? (_menuViewsContentGridCommand = new RelayCommand(OnMenuViewsContentGrid));

        public ICommand MenuViewsDataGridCommand => _menuViewsDataGridCommand ?? (_menuViewsDataGridCommand = new RelayCommand(OnMenuViewsDataGrid));

        public ICommand MenuViewsTabbedPivotCommand => _menuViewsTabbedPivotCommand ?? (_menuViewsTabbedPivotCommand = new RelayCommand(OnMenuViewsTabbedPivot));

        public ICommand MenuViewsImageGalleryCommand => _menuViewsImageGalleryCommand ?? (_menuViewsImageGalleryCommand = new RelayCommand(OnMenuViewsImageGallery));

        public ICommand MenuFileSettingsCommand => _menuFilesSettingsCommand ?? (_menuFilesSettingsCommand = new RelayCommand(OnMenuFileSettings));

        public ICommand MenuFileExitCommand => _menuFileExitCommand ?? (_menuFileExitCommand = new RelayCommand(OnMenuFileExit));

        public ShellViewModel()
        {
        }

        public void Initialize(Frame shellFrame, SplitView splitView, Frame rightFrame, IList<KeyboardAccelerator> keyboardAccelerators)
        {
            NavigationService.Frame = shellFrame;
            MenuNavigationHelper.Initialize(splitView, rightFrame);
            _keyboardAccelerators = keyboardAccelerators;
        }

        private void OnLoaded()
        {
            // Keyboard accelerators are added here to avoid showing 'Alt + left' tooltip on the page.
            // More info on tracking issue https://github.com/Microsoft/microsoft-ui-xaml/issues/8
            _keyboardAccelerators.Add(_altLeftKeyboardAccelerator);
            _keyboardAccelerators.Add(_backKeyboardAccelerator);
        }

        private void OnMenuFileNewConfigFile() => MenuNavigationHelper.UpdateView(typeof(ConfigurationsPage));

        private async void OnMenuFileOpenConfigFile()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".conf");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                string filePath = file.Path;
                ConfigFile configFile = new ConfigFile(filePath);
                List<ConfigurationItem> configurations = configFile.ReadFile();
                // Application now has read/write access to the picked file
                MenuNavigationHelper.UpdateView(typeof(ConfigurationsPage), configurations);
                //this.textBlock.Text = "Picked photo: " + file.Name;
            }
            else
            {
                //this.textBlock.Text = "Operation cancelled.";
            }

            //var fileContent = string.Empty;


            //OpenFileDialog openFileDialog = new OpenFileDialog()
            //{
            //    InitialDirectory = "c:\\",
            //    Filter = "DOSBox-X config files (*.conf)|*.conf|All files (*.*)|*.*",
            //    FilterIndex = 1,
            //    RestoreDirectory = true
            //};
            //bool? result = openFileDialog.ShowDialog();
            //if (result == true)
            //{

            //    
            //    //_navigationService.NavigateTo(typeof(MainViewModel).FullName, configurations, true);
            //    //    //LoadConfigurations(configurations);




            //}
            
        }

        private void OnMenuViewsMain() => MenuNavigationHelper.UpdateView(typeof(MainPage));

        private void OnMenuViewsListDetails() => MenuNavigationHelper.UpdateView(typeof(ListDetailsPage));

        private void OnMenuViewsContentGrid() => MenuNavigationHelper.UpdateView(typeof(ContentGridPage));

        private void OnMenuViewsDataGrid() => MenuNavigationHelper.UpdateView(typeof(DataGridPage));

        private void OnMenuViewsTabbedPivot() => MenuNavigationHelper.UpdateView(typeof(TabbedPivotPage));

        private void OnMenuViewsImageGallery() => MenuNavigationHelper.UpdateView(typeof(ImageGalleryPage));

        private void OnMenuFileSettings() => MenuNavigationHelper.OpenInRightPane(typeof(SettingsPage));

        private void OnMenuFileExit()
        {
            Application.Current.Exit();
        }

        private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
        {
            var keyboardAccelerator = new KeyboardAccelerator() { Key = key };
            if (modifiers.HasValue)
            {
                keyboardAccelerator.Modifiers = modifiers.Value;
            }

            keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;
            return keyboardAccelerator;
        }

        private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            var result = NavigationService.GoBack();
            args.Handled = result;
        }
    }
}
