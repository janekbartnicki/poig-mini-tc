using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MiniTC.Models
{
    public class FileSystemService
    {
        public IEnumerable<string> GetLogicalDrives()
        {
            return DriveInfo.GetDrives()
                            .Where(d => d.IsReady)
                            .Select(d => d.Name);
        }

        public IEnumerable<string> GetDirectoryItems(string path)
        {
            var items = new List<string>();

            try
            {
                if (!IsRootDirectory(path))
                {
                    items.Add("..");
                }

                var dirs = Directory.GetDirectories(path);
                foreach (var dir in dirs)
                {
                    var name = "<D> " + Path.GetFileName(dir);
                    items.Add(name);
                }

                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    items.Add(Path.GetFileName(file));
                }
            }
            catch
            {
                items.Add("[Błąd podczas odczytu]");
            }

            return items;
        }

        public string? GetParentDirectory(string path)
        {
            try
            {
                return Directory.GetParent(path)?.FullName;
            }
            catch
            {
                return null;
            }
        }

        public string? CombinePath(string currentPath, string selectedItem)
        {
            if (selectedItem == "..")
            {
                return GetParentDirectory(currentPath);
            }

            string candidate = Path.Combine(currentPath, selectedItem);
            if (Directory.Exists(candidate))
                return candidate;

            return null;
        }

        public bool IsRootDirectory(string path)
        {
            try
            {
                return Directory.GetParent(path) == null;
            }
            catch
            {
                return true;
            }
        }

        public void CopyFile(string sourcePath, string targetPath)
        {
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException("Plik źródłowy nie istnieje.");

            var fileName = Path.GetFileName(sourcePath);
            var destination = Path.Combine(targetPath, fileName);
            File.Copy(sourcePath, destination, overwrite: true);
        }
    }
}
