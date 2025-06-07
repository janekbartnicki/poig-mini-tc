using MiniTC.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Interfaces
{
    public interface IPanelTC
    {
        void SetLogicalDrives(IEnumerable<string> drives);
        void SetDirectoryItems(IEnumerable<string> items);
        string? CurrentPath { get; set; }
        string? SelectedItem { get; }
    }

}
