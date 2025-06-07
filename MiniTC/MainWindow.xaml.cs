using MiniTC.Interfaces;
using MiniTC.Models;
using MiniTC.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiniTC;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private PanelTC? _activePanel;

    public MainWindow()
    {
        InitializeComponent();

        LeftPanel.PanelClicked += OnPanelClicked;
        RightPanel.PanelClicked += OnPanelClicked;
    }

    private void OnPanelClicked(PanelTC panel)
    {
        _activePanel = panel;
    }

    private void CopyButton_Click(object sender, RoutedEventArgs e)
    {
        if (_activePanel == null)
        {
            MessageBox.Show("Najpierw wybierz panel źródłowy (kliknij w listę).");
            return;
        }

        var sourcePanel = _activePanel;
        var targetPanel = sourcePanel == LeftPanel ? RightPanel : LeftPanel;

        string? selectedItem = sourcePanel.SelectedItem;
        string? sourcePath = sourcePanel.CurrentPath;
        string? targetPath = targetPanel.CurrentPath;

        if (string.IsNullOrEmpty(selectedItem) || string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(targetPath))
        {
            MessageBox.Show("Brak danych do kopiowania.");
            return;
        }

        if (selectedItem.StartsWith("<D> ") || selectedItem == "..")
        {
            MessageBox.Show("Kopiowanie folderów nieobsługiwane.");
            return;
        }

        string fullSource = System.IO.Path.Combine(sourcePath, selectedItem);

        try
        {
            var fs = new FileSystemService();
            fs.CopyFile(fullSource, targetPath);

            if (targetPanel is IPanelTC panel)
            {
                var fsItems = fs.GetDirectoryItems(targetPath);
                panel.SetDirectoryItems(fsItems);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd podczas kopiowania: " + ex.Message);
        }
    }
}
