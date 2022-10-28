using CommunityToolkit.Mvvm.ComponentModel;
using SmartConf.WPF.Contracts.ViewModels;
using SmartConf.WPF.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConf.WPF.ViewModels
{
    public class ConfigurationsViewModel : ObservableObject, INavigationAware
    {
        public ConfigurationsViewModel()
        {

        }

        #region Properties

        private ConfigurationItem my;
        public ConfigurationItem My
        {
            get { return my; }
            set { SetProperty(ref my, value); }
        }

        private string zzzz;
        public string Zzzz
        {
            get { return zzzz; }
            set { SetProperty(ref zzzz, value); }
        }

        #endregion

        #region Events

        #endregion

        #region Methods

        #endregion

        public void OnNavigatedTo(object parameter)
        {

            if (parameter is List<ConfigurationItem> configurations)
            {

                My = configurations[1];
                Zzzz = My.Name;

            }
        }

        public void OnNavigatedFrom()
        {

        }

    }

}
