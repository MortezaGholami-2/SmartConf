using SmartConf.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartConf.WPF.Views
{
    /// <summary>
    /// Interaction logic for ConfigurationsPage.xaml
    /// </summary>
    public partial class ConfigurationsPage : Page
    {
        public ConfigurationsViewModel ViewModel { get; } = new ConfigurationsViewModel();

        public ConfigurationsPage()
        {
            InitializeComponent();
        }
    }
}
