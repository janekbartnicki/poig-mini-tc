using MiniTC.Presenters;
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

namespace MiniTC.Views
{
    /// <summary>
    /// Logika interakcji dla klasy PanelTC.xaml
    /// </summary>
    public partial class PanelTC : UserControl
    {
        private PanelTCPresenter _presenter;
        public IEnumerable<string> LogicalDrives => _presenter.GetLogicalDrives();
        public IEnumerable<string> DirectoryItems { get; set; }

        public PanelTC()
        {
            InitializeComponent();
            _presenter = new PanelTCPresenter();
            this.DataContext = this;
        }

        public void OnDriveSelected(object sender, SelectionChangedEventArgs e)
        {
            if (DrivesComboBox.SelectedItem is string selectedDrive)
            {
                DirectoryItems = _presenter.GetDirectoryItems(selectedDrive);
                ItemsListBox.ItemsSource = DirectoryItems;
            }
        }
    }
}
