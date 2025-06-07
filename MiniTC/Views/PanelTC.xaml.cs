using MiniTC.Interfaces;
using MiniTC.Presenters;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MiniTC.Views
{
    public partial class PanelTC : UserControl, IPanelTC
    {
        private readonly PanelTCPresenter _presenter;
        public string? SelectedItem => ItemsListBox.SelectedItem as string;

        private string? _currentPath;
        public string? CurrentPath
        {
            get => _currentPath;
            set
            {
                _currentPath = value;
                PathTextBox.Text = value ?? string.Empty;
            }
        }

        public event Action<PanelTC>? PanelClicked;

        public PanelTC()
        {
            InitializeComponent();
            _presenter = new PanelTCPresenter(this);
        }

        private void RaisePanelClicked()
        {
            PanelClicked?.Invoke(this);
        }

        public void SetLogicalDrives(IEnumerable<string> drives)
        {
            DrivesComboBox.ItemsSource = drives;
        }

        public void SetDirectoryItems(IEnumerable<string> items)
        {
            ItemsListBox.ItemsSource = items;
        }

        private void DrivesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DrivesComboBox.SelectedItem is string selectedDrive)
            {
                _presenter.OnDriveSelected(selectedDrive);
            }
        }

        private void ItemsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ItemsListBox.SelectedItem is string selectedItem && CurrentPath != null)
            {
                _presenter.OnFileSelected(CurrentPath, selectedItem);
            }
        }

        private void ItemsListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RaisePanelClicked();
        }
    }
}
