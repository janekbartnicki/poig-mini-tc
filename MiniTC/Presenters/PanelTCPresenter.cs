using MiniTC.Interfaces;
using MiniTC.Models;
using System.Collections.Generic;
using System.Windows;

namespace MiniTC.Presenters
{
    public class PanelTCPresenter
    {
        private readonly IPanelTC _view;
        private readonly FileSystemService _fsService;

        public PanelTCPresenter(IPanelTC view)
        {
            _view = view;
            _fsService = new FileSystemService();
            Init();
        }

        private void Init()
        {
            IEnumerable<string> drives = _fsService.GetLogicalDrives();
            _view.SetLogicalDrives(drives);
        }

        public void OnDriveSelected(string drive)
        {
            var items = _fsService.GetDirectoryItems(drive);
            _view.SetDirectoryItems(items);
            _view.CurrentPath = drive;
        }

        public void OnFileSelected(string currentPath, string file)
        {
            if (string.IsNullOrEmpty(currentPath) || string.IsNullOrEmpty(file))
                return;
            
            bool isDirectory = file.StartsWith("<D> ") || file == "..";

            if (file.StartsWith("<D> "))
                file = file.Substring(4);

            if (isDirectory)
            {
                var combinedPath = _fsService.CombinePath(currentPath, file);

                if (combinedPath == null)
                {
                    MessageBox.Show("Nie można otworzyć katalogu", "Błąd");
                    return;
                }

                var items = _fsService.GetDirectoryItems(combinedPath);
                _view.SetDirectoryItems(items);
                _view.CurrentPath = combinedPath;
            }
        }

    }
}
