using MiniTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Presenters
{
    class PanelTCPresenter
    {
        private readonly FileSystemService _fsService;

        public PanelTCPresenter(FileSystemService fsService)
        {
            _fsService = fsService;
        }

        public PanelTCPresenter() : this(new FileSystemService()) {}

        public IEnumerable<string> GetLogicalDrives()
        {
            return _fsService.GetLogicalDrives();
        }

        public IEnumerable<string> GetDirectoryItems(string path)
        {
            return _fsService.GetDirectoryItems(path);
        }
    }
}
